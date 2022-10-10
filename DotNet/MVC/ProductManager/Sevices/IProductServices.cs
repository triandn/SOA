using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Models;

namespace ProductManager.Services
{
    public interface IProductServices
    {
        List<Product>  getProducts();

        List<ProductView> GetProductAll();
        Product? GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<Category> GetCategories();

        string GetCategoryName(int id);
    }
}