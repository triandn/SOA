using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        public byte[] PassWordHash { get; set;}

        public byte[] PasswordSalt { get; set; }

    }

}