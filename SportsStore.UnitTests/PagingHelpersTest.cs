using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SportsStore.UnitTests
{
    [TestClass]
    public class PagingHelpersTest
    {
        [TestMethod]
        public void PageLinks_3Pages_ProduceCorectHtml()
        {
            // Arrange
            HtmlHelper html = null;
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                ItemsPerPage = 2,
                TotalItems = 5,
            };
            Func<int, string> helperfunc = i => "Page" + i;

            // Act
            var htmlResult = html.PageLinks(pagingInfo, helperfunc);

            // Assert
            Assert.AreEqual( @"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" 
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                htmlResult.ToString());
        }
    }
}
