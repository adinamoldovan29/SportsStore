using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Controllers;
using System.Linq;
using SportsStore.WebUI.Models;
using SportsStore.UnitTests.Helpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void List_Pagination_ReturnsProductsPaginated()
        {
            // Arange
            var productsRepoMoack = RepositoryHelper.CreateProductsRepoMock();

            var productController = new ProductController(productsRepoMoack.Object);
            productController.PageSize = 2;

            // Act
            var model = productController.List(null, 2).Model as ProductsListViewModel;

            // Assert
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(1, model.Products.Count());
            Assert.AreEqual("P3", model.Products.First().Name);
        }

        [TestMethod]
        public void List_Pagination_ReturnsPagingInfoCorrect()
        {
            // Arange
            var productsRepoMoack = RepositoryHelper.CreateProductsRepoMock();

            var productController = new ProductController(productsRepoMoack.Object);
            productController.PageSize = 2;

            // Act
            var model = productController.List(null, 2).Model as ProductsListViewModel;

            // Assert
            Assert.IsNotNull(model, "Model is null");

            Assert.AreEqual(2, model.PagingInfo.CurrentPage, "CurrentPage is not correct");
            Assert.AreEqual(2, model.PagingInfo.TotalPages, "TotalPages is not correct");
            Assert.AreEqual(3, model.PagingInfo.TotalItems, "TotalItem value is not correct");
            Assert.AreEqual(2, model.PagingInfo.ItemsPerPage, "ItemPerPage value is not correct");
        }

        [TestMethod]
        public void List_Category_ReturnsOnlyProductsFromCategory()
        {
            var category = "cat1";
            // Arange
            var productsRepoMoack = RepositoryHelper.CreateProductsWithCategoryRepoMock();

            var productController = new ProductController(productsRepoMoack.Object);
            productController.PageSize = 2;

            // Act
            var model = productController.List(category, 1).Model as ProductsListViewModel;

            // Assert
            Assert.IsNotNull(model, "Model is null");

            Assert.AreEqual(2, model.Products.Count(), "Products were not retured corectly");
            Assert.AreEqual(category, model.Products.First().Category, "Category is not correct");
            Assert.AreEqual(category, model.Products.Last().Category, "Category is not correct");
        }

        [TestMethod]
        public void List_CategoryNull_ReturnsAllProducts()
        {
            // Arange
            var productsRepoMoack = RepositoryHelper.CreateProductsWithCategoryRepoMock().Object;
            var productController = new ProductController(productsRepoMoack);

            // Act
            var model = productController.List(null, 1).Model as ProductsListViewModel;

            // Assert
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(productsRepoMoack.Products.Count(), model.PagingInfo.TotalItems, "Products were not retured corectly");
        }
    }
}
