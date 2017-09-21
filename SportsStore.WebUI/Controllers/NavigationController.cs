using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IProductsRepository prodRepo;

        public NavigationController(IProductsRepository prodRepository)
        {
            prodRepo = prodRepository;
        }

        // GET: Navigation
        public PartialViewResult Menu()
        {
            var cat = prodRepo.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(cat);
        }
    }
}