using Microsoft.EntityFrameworkCore;
using ProductCoreAPI.DBContext;
using ProductCoreAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductCoreAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDbContext _productDbContext;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public Product AddProduct(Product product)
        {
            _productDbContext.Products.Add(product);            
            _productDbContext.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts()
        {
            return _productDbContext.Products.Include(p=>p.ProductCategory).ToList();
        }

        public Product GetProductById(int id)
        {
            return _productDbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateProduct(int id, Product product)
        {
            bool isProductUpdated = false;
            var productTobeUpdated = _productDbContext.Products.FirstOrDefault(p => p.Id == id);



            if (productTobeUpdated != null)
            {
                productTobeUpdated.ProductName = product.ProductName;
                productTobeUpdated.ProductOrigin = product.ProductOrigin;
                productTobeUpdated.ProductPrice = product.ProductPrice;
                _productDbContext.SaveChanges();
                isProductUpdated = true;

            }

            return isProductUpdated;

        }
        public bool DeleteProduct(int id)
        {
            bool isProductRemoved = false;
            var productTobeRemoved = _productDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (productTobeRemoved != null)            
            {
                _productDbContext.Products.Remove(productTobeRemoved);
                _productDbContext.SaveChanges();
                isProductRemoved = true;
            }
            return isProductRemoved;
        }

      

      
    }
}
