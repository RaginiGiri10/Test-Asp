using ProductCoreAPI.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCoreAPI.Repository.ProductCategory
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductCategoryRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public List<Models.ProductCategory> GetProductCategories()
        {
            var productCategories = _productDbContext.ProductCategories.ToList();           
            return productCategories;
        }
    }
}
