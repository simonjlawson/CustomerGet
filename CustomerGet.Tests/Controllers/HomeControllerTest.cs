using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerGet.Controllers;
using CustomerGet.Business.Functions;

namespace CustomerGet.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var testCusomterDataFactory = new TestCustomerDataFactory();
            var controller = new HomeController(testCusomterDataFactory);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
