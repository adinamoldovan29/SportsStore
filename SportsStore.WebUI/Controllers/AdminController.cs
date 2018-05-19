using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductsRepository productRepository;

        public AdminController(IProductsRepository repo)
        {
            productRepository = repo;
        }
        
        [HttpGet]
        public ViewResult Index()
        {
            return View(productRepository.Products);
        }

        [HttpGet]
        public ViewResult Edit(int ProductID)
        {
            var selectedProd = productRepository.Products
                .FirstOrDefault(p => p.ProductID == ProductID);

            return View(selectedProd);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMymeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                productRepository.SaveProduct(product);
                TempData["message"] = $"The product {product.Name} has been saved";

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View("Edit", new Product()); 
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var deletedProd = productRepository.DeleteProduct(productId);
            if (deletedProd != null)
            {
                TempData["message"] = $"Product {deletedProd.Name} was deleted";
            }
            
            return RedirectToAction("Index");
        }
    }
}