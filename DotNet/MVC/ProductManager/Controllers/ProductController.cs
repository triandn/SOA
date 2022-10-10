using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productService;
        public ProductController(IProductServices productServices)
        {
            _productService = productServices;
        }
        public IActionResult Index()
        {   
            var products = _productService.getProducts();
            var productView = _productService.GetProductAll();
            return View(products);
        }
        public IActionResult Create()
        {
            var categories = _productService.GetCategories();
            return View(categories);
        }
        public IActionResult SaveProduct(Product product){
            if (product.Id == 0){
                _productService.CreateProduct(product);
            }
            else{
                _productService.UpdateProduct(product);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id){
            var product = _productService.GetProductById(id);
            if (product == null) return RedirectToAction("Create");
            var categories = _productService.GetCategories();
            ViewBag.Products = product; //

            return View(categories);
        }
        public IActionResult Delete(int id){
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }

}