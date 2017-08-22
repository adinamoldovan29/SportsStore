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

        public ProductController(IProductsRepository repository)
        {
            productRepository = repository;
        }

        public ViewResult List()
        {
            return View(productRepository.Products);
        }
    }
}