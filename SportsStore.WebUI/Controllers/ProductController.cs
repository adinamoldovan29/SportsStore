using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository productRepository;
        public int PageSize = 4;

        public ProductController(IProductsRepository repository)
        {
            productRepository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var productsForCategory = string.IsNullOrWhiteSpace(category) ?
                productRepository.Products :
                productRepository.Products.Where(p => p.Category == category);

            var productsForPage = productsForCategory
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = productsForCategory.Count(),
            };

            var model = new ProductsListViewModel()
            {
                Products = productsForPage,
                PagingInfo = pagingInfo,
                CurrentCategory = category,
            };
            return View(model);
        }

        [HttpGet]
        public FileContentResult GetImage(int productID)
        {
            var selectedProduct = productRepository.Products
             .FirstOrDefault(p => p.ProductID == productID);

            if (selectedProduct != null)
            {
                return File(selectedProduct.ImageData, selectedProduct.ImageMymeType);
            }
            else
            {
                return null;
            }
        }
    }
}