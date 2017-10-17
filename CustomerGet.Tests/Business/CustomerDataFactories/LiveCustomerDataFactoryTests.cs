using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerGet.Controllers;
using CustomerGet.Business.Functions;
using System.Net.Http;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Moq;

namespace CustomerGet.Tests.Business.CustomerDataFactories
{
    [TestClass]
    public class CustomerDataFactoryTests
    {
        [TestMethod]
        public void GetAllReturnsAllData()
        {
            // Arrange
            var httpClient = new HttpClient();

            var customersJson = File.ReadAllText(@"Business\CustomerDataFactories\Json\customers.json");
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAllAsync())
                .Returns(Task.FromResult(customersJson));

            var factory = new LiveCustomerDataFactory(customerApi.Object);

            // Act
            var customers = factory.GetAll();

            // Assert
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.ListOfCustomers.Any());
        }

        [TestMethod]
        public void GetAllReturnsEmptyCustomersWithNoApiResponse()
        {
            // Arrange
            var httpClient = new HttpClient();

            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAllAsync())
                .Returns(Task.FromResult(string.Empty));

            var factory = new LiveCustomerDataFactory(customerApi.Object);

            // Act
            var customers = factory.GetAll();

            // Assert
            Assert.IsNotNull(customers);
            Assert.IsNotNull(customers.ListOfCustomers);
            Assert.IsFalse(customers.ListOfCustomers.Any());
        }

        [TestMethod]
        public void GetAllReturnsEmptyCustomersWithInvalidApiResponse()
        {
            // Arrange
            var httpClient = new HttpClient();

            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAllAsync())
                .Returns(Task.FromResult("<Invalid>Response</Invalid>"));

            var factory = new LiveCustomerDataFactory(customerApi.Object);

            // Act
            var customers = factory.GetAll();

            // Assert
            Assert.IsNotNull(customers);
            Assert.IsNotNull(customers.ListOfCustomers);
            Assert.IsFalse(customers.ListOfCustomers.Any());
        }

        [TestMethod]
        public void GetCustomerReturnsData()
        {
            // Arrange
            var httpClient = new HttpClient();

            var customersJson = File.ReadAllText(@"Business\CustomerDataFactories\Json\customer.json");
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customersJson));

            var factory = new LiveCustomerDataFactory(customerApi.Object);

            // Act
            var customer = factory.Get(new Guid());

            // Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.FirstName, "David");
        }

        [TestMethod]
        public void GetCustomerReturnsEmptyCustomerWithNoApiResponse()
        {
            // Arrange
            var httpClient = new HttpClient();

            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(string.Empty));

            var factory = new LiveCustomerDataFactory(customerApi.Object);

            // Act
            var customer = factory.Get(new Guid());

            // Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.id, new Guid().ToString());
        }

        [TestMethod]
        public void GetCustomerReturnsEmptyCustomerWithInvalidApiResponse()
        {
            // Arrange
            var httpClient = new HttpClient();

            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult("<Invalid>Response</Invalid>"));

            var factory = new LiveCustomerDataFactory(customerApi.Object);

            // Act
            var customer = factory.Get(new Guid());

            // Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.id, new Guid().ToString());
        }
    }
}
