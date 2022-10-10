using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManager.Models;


namespace ProductManager.Services
{
    public class ProductService : IProductServices
    {
        private readonly DataContext _context;
        public ProductService(DataContext context){
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var existedProduct = GetProductById(id);
            if ( existedProduct == null) return;
            _context.Products.Remove(existedProduct);
            _context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return _context.Category.ToList();
        }

        public string GetCategoryName(int id)
        {
            String categoryName = _context.Category.Where(c=>c.Id == id).First().Name;

            return categoryName;
        }

        public List<ProductView> GetProductAll()
        {
            var result = (from p in _context.Products
                        join c in _context.Category on p.CategoryId equals c.Id
                        select new ProductView{
                            Id = p.Id,
                            Name = p.Name,
                            Slug = p.Slug,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            CategoryName = c.Name
                        }).ToList();
            return result;
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> getProducts()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return products;
        }

        public void UpdateProduct(Product product)
        {
            var existedProduct = GetProductById(product.Id);
            if (existedProduct == null) return;
            existedProduct.Name = product.Name;
            existedProduct.Price = product.Price;
            existedProduct.Slug = product.Slug;
            existedProduct.Quantity = product.Quantity;
            existedProduct.CategoryId = product.CategoryId;
            _context.Products.Update(existedProduct);
            _context.SaveChanges();
        }
    }
}