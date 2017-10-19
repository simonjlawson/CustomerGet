using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerGet.Controllers;
using CustomerGet.Business.Functions;
using Moq;
using System.IO;
using System.Threading.Tasks;
using CustomerGet.Models;
using System.Linq;

namespace CustomerGet.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void HomeControllerCompletesAndViewModelWithCustomerObjects()
        {
            // Arrange
            var customersJson = File.ReadAllText(@"Business\CustomerDataFactories\Json\customers.json");
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAllAsync())
                .Returns(Task.FromResult(customersJson));

            var factory = new LiveCustomerDataFactory(customerApi.Object);
            var controller = new HomeController(factory);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is HomeViewModel);
            Assert.IsTrue(((HomeViewModel)result.Model).Customers.Any());
        }

        [TestMethod]
        public void HomeControllerCompletesIfNoCustomersAreFound()
        {
            // Arrange
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAllAsync())
                .Returns(Task.FromResult("<Invalid>Text</Invalid>"));

            var factory = new LiveCustomerDataFactory(customerApi.Object);
            var controller = new HomeController(factory);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is HomeViewModel);
            Assert.IsTrue(((HomeViewModel)result.Model).Customers != null);
            Assert.IsFalse(((HomeViewModel)result.Model).Customers.Any());
        }

        [TestMethod]
        public void HomeControllerCustomerCompletesAndListOneCustomerForCustomerView()
        {
            // Arrange
            var customerJson = File.ReadAllText(@"Business\CustomerDataFactories\Json\customer.json");
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customerJson));

            var factory = new LiveCustomerDataFactory(customerApi.Object);
            var controller = new HomeController(factory);

            // Act
            ViewResult result = controller.Customer(new Guid().ToString()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is CustomerViewModel);
            Assert.IsTrue(((CustomerViewModel)result.Model).Customer.id != null);
        }

        [TestMethod]
        public void HomeControllerCustomerCompletesAndListAnEmptyIfInvalid()
        {
            // Arrange
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult("<Invalid>Text</Invalid>"));

            var factory = new LiveCustomerDataFactory(customerApi.Object);
            var controller = new HomeController(factory);

            // Act
            ViewResult result = controller.Customer(new Guid().ToString()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is CustomerViewModel);
            Assert.IsTrue(((CustomerViewModel)result.Model).Customer != null);
        }
    }
}
