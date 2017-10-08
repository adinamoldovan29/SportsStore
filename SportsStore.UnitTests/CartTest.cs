﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System.Linq;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTest
    {
        #region Cart Entity
        [TestMethod]
        public void AddToCart_2Products_Added()
        {
            // Arange
            var prod1 = new Product() { ProductID = 1, Name = "ProdName1" };
            var prod2 = new Product() { ProductID = 2, Name = "ProdName2" };
            var cart = new Cart();

            // Act
            cart.AddToCart(prod1, 2);
            cart.AddToCart(prod2, 4);

            // Assert
            Assert.AreEqual(2, cart.CartLines.Count(), "The number of CartLines is not correct");
            Assert.AreEqual(2, cart.CartLines.Where(_ => _.Product == prod1).Single().Quantity, "Product1 quantity is not correct");
            Assert.AreEqual(4, cart.CartLines.Where(_ => _.Product == prod2).Single().Quantity, "Product2 quantity is not correct");
        }

        [TestMethod]
        public void AddToCart_ExistingProducts_IncreaseQuantity()
        {
            var prod1 = new Product() { ProductID = 1, Name = "ProdName1" };
            var prod2 = new Product() { ProductID = 2, Name = "ProdName2" };
            var cart = new Cart();

            // Act
            cart.AddToCart(prod1, 2);
            cart.AddToCart(prod2, 4);
            cart.AddToCart(prod1, 3);
            cart.AddToCart(prod2, 2);

            // Assert
            Assert.AreEqual(2, cart.CartLines.Count(), "The number of CartLines is not correct");
            Assert.AreEqual(5, cart.CartLines.Where(_ => _.Product == prod1).Single().Quantity, "Product1 quantity is not correct");
            Assert.AreEqual(6, cart.CartLines.Where(_ => _.Product == prod2).Single().Quantity, "Product2 quantity is not correct");
        }

        [TestMethod]
        public void ClearCart_2Lines_CartEmpty()
        {
            // Arange
            var prod1 = new Product() {ProductID = 1, Name = "ProdName1"};
            var prod2 = new Product() {ProductID = 2, Name = "ProdName2"};

            var cart = new Cart();
            cart.AddToCart(prod1, 2);
            cart.AddToCart(prod2, 5);

            // Act
            cart.ClearCart();

            // Assert
            Assert.AreEqual(0, cart.CartLines.Count(), "The number of CartLines is not correct");
        }

        [TestMethod]
        public void RemoveLine_ManyProducts_ProductRemoved()
        {
            // Arange
            var prod1 = new Product() { ProductID = 1, Name = "ProdName1" };
            var prod2 = new Product() { ProductID = 2, Name = "ProdName2" };
            var prod3 = new Product() { ProductID = 3, Name = "ProdName3" };

            var cart = new Cart();
            cart.AddToCart(prod3, 1);
            cart.AddToCart(prod1, 2);
            cart.AddToCart(prod2, 5);
            cart.AddToCart(prod3, 1);

            // Act
            cart.RemoveLine(prod3);

            // Assert
            Assert.AreEqual(0, cart.CartLines.Where(pl => pl.Product == prod3).Count(), "Produc was not removed from cart");
        }

        [TestMethod]
        public void ComputeCartValue_2Products_CorrectValue()
        {
            // Arange
            var prod1 = new Product() { ProductID = 1, Name = "ProdName1", Price = 10 };
            var prod2 = new Product() { ProductID = 2, Name = "ProdName2", Price = 15 };

            var cart = new Cart();
            cart.AddToCart(prod1, 2);
            cart.AddToCart(prod2, 1);
            cart.AddToCart(prod1, 2);

            // Act
            var cartValue = cart.ComputeCartValue();

            // Assert
            Assert.AreEqual(55, cartValue, "Cart value not calculated correct");
        }
        #endregion

        #region CartController

        [TestMethod]
        public void AddToCart_OneProduct_ProductAdded()
        {
            // Arange
            var prodRepoMock = new Mock<IProductsRepository>();
            prodRepoMock.Setup(m => m.Products).Returns(new Product[] {
            new Product() { ProductID = 1, Name = "P1", Category = "Products"}}.AsQueryable());

            var cart = new Cart();
            var cartController = new CartController(prodRepoMock.Object);

            // Act
            cartController.AddtoCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.CartLines.Count(), 1);
            Assert.AreEqual(cart.CartLines.First().Product.ProductID, 1);
        }


        #endregion
    }
}