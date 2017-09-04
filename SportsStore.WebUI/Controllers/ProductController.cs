using SportsStore.Domain.Abstract;
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

        public ViewResult List(int page = 1)
        {
            return View(productRepository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page -1) * PageSize)
                .Take(PageSize));
        }
    }
}