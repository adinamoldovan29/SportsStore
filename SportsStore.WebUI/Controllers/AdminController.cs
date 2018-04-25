﻿using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
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
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.SaveProduct(product);
                TempData["message"] = $"The product {product.Name} has been saved";

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
    }
}