using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCoreAPI.Repository.ProductCategory
{
    public interface IProductCategoryRepository
    {
        List<ProductCoreAPI.Models.ProductCategory> GetProductCategories();
    }
}
