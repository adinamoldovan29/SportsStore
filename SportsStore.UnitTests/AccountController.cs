using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login_CorrectUser_UserAuthenticated()
        {
            var authProviderMock = new Mock<IAuthProvider>();
            authProviderMock.Setup(m => m.Authenticate("goodUser", "goodPass"))
                .Returns(true);

            var controller = new AccountController(authProviderMock.Object);
            var loginViewModel = 
                new LoginViewModel() { Username = "goodUser", Password = "goodPass" };

            var result = controller.Login(loginViewModel, "Url");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("Url", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Login_InvalidUser_Error()
        {
            var authProviderMock = new Mock<IAuthProvider>();
            authProviderMock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            var controller = new AccountController(authProviderMock.Object);
            var loginViewModel =
                new LoginViewModel() { Username = "badUser", Password = "badPass" };

            var result = controller.Login(loginViewModel, null);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse( ((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
