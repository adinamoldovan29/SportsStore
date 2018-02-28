using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductsRepository repository, IOrderProcessor orderProcessor) { 
            this.repository = repository;
            this.orderProcessor = orderProcessor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel(){
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
  
        public RedirectToRouteResult AddtoCart(Cart cart, int productId, string returnUrl) { 
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddToCart(product, 1);
             }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(){
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) {
            if (cart.CartLines.Count() == 0) {
                ModelState.AddModelError("", "Sorry youe cart is empty.");
            }

            if (ModelState.IsValid){
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.ClearCart();
                return View("Completed");
            }
            else{
                return View(shippingDetails);
            }
        }
    }
}