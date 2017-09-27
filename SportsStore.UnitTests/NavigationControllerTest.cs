using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.UnitTests.Helpers;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class NavigationControllerTest
    {
        [TestMethod]
        public void Menu_ProductsWithCategories_CreateCategoriesList()
        {
            var productsMock = RepositoryHelper.CreateProductsWithCategoryRepoMock();

            var navController = new NavigationController(productsMock.Object);
            var menuElements = (navController.Menu().Model as IEnumerable<string>).ToList();

            Assert.AreEqual(2, menuElements.Count());
            Assert.IsTrue(menuElements.Any(c => c == "cat1"));
            Assert.IsTrue(menuElements.Any(c => c == "cat2"));
        }

        [TestMethod]
        public void Menu_CurrentCategory_IndicatesCategoryCorrectly()
        {
            var productsMock = RepositoryHelper.CreateProductsWithCategoryRepoMock();
            var currentCategory = "cat1";

            var navController = new NavigationController(productsMock.Object);
            var menu = navController.Menu(currentCategory);

            Assert.AreEqual(currentCategory, menu.ViewBag.CurrentCategory);
        }
    }
}
