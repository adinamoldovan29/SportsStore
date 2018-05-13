using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.UnitTests.Helpers;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void Index_ManyProducts_ReturnsAll()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);

            var result = (IEnumerable<Product>)adminController.Index().ViewData.Model;

            Assert.AreEqual(3, result.Count(), "Not all the product were rwturned");
        }

        [TestMethod]
        public void Index_ManyProducts_ReturnsCorrectProducts()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);

            var result = ((IEnumerable<Product>)adminController.Index().ViewData.Model).ToArray();

            Assert.AreEqual("P1", result[2].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[0].Name);
        }

        [TestMethod]
        public void Edit_OneProductRequested_ProductReturnedToView()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);

            var resultProduct = adminController.Edit(2).ViewData.Model as Product;

            Assert.AreEqual("P2", resultProduct.Name);
        }

        [TestMethod]
        public void Edit_InexistingProductRequested_NullReturnedToView()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);

            var resultProduct = adminController.Edit(10).ViewData.Model as Product;

            Assert.IsNull(resultProduct);
        }

        [TestMethod]
        public void Edit_ValidProduct_ProductUpdated()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);
            var product = new Product();

            adminController.Edit(product);

            productRepo.Verify(m => m.SaveProduct(product));
        }

        [TestMethod]
        public void Edit_InvValidProduct_ProductNotUpdated()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);
            adminController.ModelState.AddModelError("errorMessage", "This is an error");
            var product = new Product();

            adminController.Edit(product);

            productRepo.Verify(m => m.SaveProduct(product), Times.Never());
        }

        [TestMethod]
        public void Delete_ProductFromDb_ProductDeleted()
        {
            var productRepo = RepositoryHelper.CreateProductsRepoMock();
            var adminController = new AdminController(productRepo.Object);

            adminController.Delete(2);

            productRepo.Verify(m => m.DeleteProduct(2), Times.Once);
        }
    }
}
