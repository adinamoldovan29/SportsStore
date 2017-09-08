using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Linq;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void List_Pagination_ReturnsProductsPaginated()
        {
            // Arange
            var productsRepoMoack = CreateProductsRepoMock();

            var productController = new ProductController(productsRepoMoack.Object);
            productController.PageSize = 2;

            // Act
            var model = productController.List(2).Model as ProductsListViewModel;

            // Assert
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(1, model.Products.Count());
            Assert.AreEqual("P3", model.Products.First().Name);
        }

        [TestMethod]
        public void List_Pagination_ReturnsPagingInfoCorrect()
        {
            // Arange
            var productsRepoMoack = CreateProductsRepoMock();

            var productController = new ProductController(productsRepoMoack.Object);
            productController.PageSize = 2;

            // Act
            var model = productController.List(2).Model as ProductsListViewModel;

            // Assert
            Assert.IsNotNull(model, "Model is null");

            Assert.AreEqual(2, model.PagingInfo.CurrentPage, "CurrentPage is not correct");
            Assert.AreEqual(2, model.PagingInfo.TotalPages, "TotalPages is not correct");
            Assert.AreEqual(3, model.PagingInfo.TotalItems, "TotalItem value is not correct");
            Assert.AreEqual(2, model.PagingInfo.ItemsPerPage, "ItemPerPage value is not correct");
        }

        private Mock<IProductsRepository> CreateProductsRepoMock()
        {
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>(){
                new Product { ProductID = 3,  Name = "P3"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 1, Name = "P1"}
            });

            return mock;
        }
    }
}
