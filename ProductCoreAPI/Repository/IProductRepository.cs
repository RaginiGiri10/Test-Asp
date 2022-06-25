using ProductCoreAPI.Models;
using System.Collections.Generic;

namespace ProductCoreAPI.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        Product GetProductById(int id);
        Product AddProduct(Product product);

        bool DeleteProduct(int id);

        bool UpdateProduct(int id, Product product);
    }
}
