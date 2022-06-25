using ProductCoreAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductCoreAPI.ViewModels
{
    public class AddProductViewModel
    {
        [MaxLength(50)]
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }
        [Required]
        public int ProductPrice { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }

        [Required]
        public string ProductOrigin { get; set; }
       
    }
}
