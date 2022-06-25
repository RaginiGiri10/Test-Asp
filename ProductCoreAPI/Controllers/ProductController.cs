using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCoreAPI.Models;
using ProductCoreAPI.Repository;
using ProductCoreAPI.Repository.ProductCategory;
using ProductCoreAPI.ViewModels;
using System;
using System.Collections.Generic;

namespace ProductCoreAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductController(IProductRepository productRepository, 
               IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }


        [HttpPost]
        [Route("/api/product/addproduct")]
        public IActionResult AddProduct([FromBody]AddProductViewModel addProductViewModel)
        {
            Product product = new Product
            {
                ProductName = addProductViewModel.ProductName,
                ProductCategoryId = addProductViewModel.ProductCategoryId,
                ProductOrigin = addProductViewModel.ProductOrigin,
                ProductPrice = addProductViewModel.ProductPrice,
                CreatedBy = HttpContext.User.Identity.Name,
                CreatedDate = DateTime.Now
            };

            var addedPrduct = _productRepository.AddProduct(product);
            return Ok(new { Message ="Product Added"});

        }

        [HttpGet]
        [Route("/api/product/addproduct")]
        public IActionResult AddProduct()
        {
            AddProductViewModel productViewModel = new AddProductViewModel
            {
                ProductCategories = _productCategoryRepository.GetProductCategories()
            };
                          
            return Ok(productViewModel);

        }

        [HttpGet]      
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetAllProducts();
            if (products == null)
            {
                NotFound("No Products Found!!!");
            }

            List<ProductViewModel> productList = new List<ProductViewModel>();

            foreach(var product in products)
            {
                ProductViewModel productListView = new ProductViewModel
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductCategory = product.ProductCategory.CategoryName,
                    ProductOrigin = product.ProductOrigin,
                    ProductPrice = product.ProductPrice
                };

                productList.Add(productListView);
            }

            return Ok(productList);
        }


        [HttpGet("{id}")]
        
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product with id ={id} is not found");
            }

            var getProductByIdViewModel = new GetProductByIdViewModel
            {
                Id = product.Id,
                ProductCategoryId = product.ProductCategoryId,
                ProductName = product.ProductName,
                ProductOrigin = product.ProductOrigin,
                ProductPrice = product.ProductPrice,
                ProductCategories = _productCategoryRepository.GetProductCategories()
            };
            return Ok(getProductByIdViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,[FromBody]UpdateViewModel updateViewModel)
        {
            Product product = new Product
            {
                ProductName = updateViewModel.ProductName,
                ProductOrigin = updateViewModel.ProductOrigin,
                ProductPrice = updateViewModel.ProductPrice
            };

           bool isProductUpdated = _productRepository.UpdateProduct(id, product);

            if (!isProductUpdated)
            {
                return NotFound($"Product with id = {id} is not found.");
            }
            return Ok($"Product with id = {id} is updated successfully.");

        }
       


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool isProductRemoved = _productRepository.DeleteProduct(id);
            if (!isProductRemoved)
            {
                return NotFound($"Product with id = {id} is not found.");
            }
            return Ok($"Product with id = {id} is removed successfully.");
        }
    }



  
}
