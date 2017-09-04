using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void List_Pagination_ReturnsPage()
        {
            // Arange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>(){
                new Product { ProductID = 3,  Name = "P3"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 1, Name = "P1"}
            });

            var productController = new ProductController(mock.Object);
            productController.PageSize = 2;

            // Act
            var secondPage = productController.List(2).Model as IEnumerable<Product>;

            // Assert
            var secondPageProd = secondPage.ToList();
            Assert.AreEqual(1, secondPageProd.Count());
            Assert.AreEqual("P3", secondPage.First().Name);
        }
    }
}
