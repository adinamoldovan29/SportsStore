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
            var productsForCathegory = productRepository.Products
                .Where(p => string.IsNullOrWhiteSpace(category) || p.Category == category);

            var productsForPage = productsForCathegory
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = productsForCathegory.Count(),
            };

            var model = new ProductsListViewModel()
            {
                Products = productsForPage,
                PagingInfo = pagingInfo,
                CurrentCategory = category,
            };
            return View(model);
        }
    }
}