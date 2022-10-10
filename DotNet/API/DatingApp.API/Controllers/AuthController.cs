using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Data.Entity;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

        public readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AuthController(DataContext context , ITokenService tokenService){
            _tokenService = tokenService;
            _context = context;
        }
       [HttpPost("register")]
       public IActionResult Register([FromBody] AuthUserDto authUserDto)
       {
            authUserDto.UserName = authUserDto.UserName.ToLower();
            if(_context.AppUsers.Any(u => u.UserName == authUserDto.UserName))
            {
                return BadRequest("Username is already taken");
            }
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(authUserDto.Password);
            var newUser = new User{
                UserName = authUserDto.UserName,
                PasswordSalt = hmac.Key,
                PassWordHash = hmac.ComputeHash(passwordBytes),
            };

            _context.AppUsers.Add(newUser);
            _context.SaveChanges();
            var token = _tokenService.CreateToken(newUser.UserName);
            return Ok(token);
            
       }
       [HttpPost("login")]
       public IActionResult Login([FromBody] AuthUserDto authUserDto)
       {
            authUserDto.UserName = authUserDto.UserName.ToLower();
            var currentUser = _context.AppUsers
                                .FirstOrDefault(u => u.UserName == authUserDto.UserName);
            if(currentUser == null) 
            {
                return Unauthorized("Username is invalid.");
            }

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(authUserDto.Password)
            );

            for(int i = 0 ; i < currentUser.PassWordHash.Length ; i++)
            {
                if(currentUser.PassWordHash[i] != passwordBytes[i])
                {
                    return Unauthorized("Passwords do not match.");
                }
            }

            var token = _tokenService.CreateToken(currentUser.UserName);
            return Ok(token);
       }

       [HttpGet]
       public IActionResult Get()
       {
            return Ok(_context.AppUsers.ToList());
       }
    }
}