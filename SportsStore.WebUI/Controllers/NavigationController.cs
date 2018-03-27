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
        public PartialViewResult Menu(string category = null, bool isHorizontal = false)
        {
            ViewBag.CurrentCategory = category;

            var cat = prodRepo.Products
                .Select(p => p.Category)
                .Where(c => !String.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c);

            var viewName = isHorizontal ? "MenuHorizontal" : "Menu";

            return PartialView(viewName, cat);
        }
    }
}