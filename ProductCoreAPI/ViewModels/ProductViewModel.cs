using ProductCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCoreAPI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductCategory { get; set; }

        public string ProductOrigin { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }

        public int ProductPrice { get; set; }
    }
}
