using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void AddToCart_2Products_Added()
        {
            // Arange
            var prod = new Product() { Name = "ProdName" };
            var cart = new Cart();

            // Act
            cart.AddToCart(prod, 2);

            // Assert
            Assert.AreEqual(1, cart.CartLines.Count(), "The number of CartLines is not correct");
            Assert.AreEqual(2, cart.CartLines.First().Quantity, "Product quantity is not correct");
            Assert.AreEqual("ProdName", cart.CartLines.First().Product.Name, "Product name is not correct");
        }

        [TestMethod]
        public void ClearCart_2Products_CartEmpty()
        {
            // Arange
             var cart = AddProductsToCart();

            // Act
            cart.ClearCart();

            // Assert
            Assert.AreEqual(0, cart.CartLines.Count(), "The number of CartLines is not correct");
        }

        [TestMethod]
        public void RemoveLine_2Products_CartEmpty()
        {
            // Arange
            var cart = AddProductsToCart();

            // Act
            cart.RemoveLine(cart.CartLines.First().Product);

            // Assert
            Assert.AreEqual(0, cart.CartLines.Count(), "The number of CartLines is not correct");
        }

        private Cart AddProductsToCart()
        {
            var prod = new Product() { Name = "ProdName" };

            var cart = new Cart();
            cart.AddToCart(prod, 2);

            return cart;
        }
    }
}
