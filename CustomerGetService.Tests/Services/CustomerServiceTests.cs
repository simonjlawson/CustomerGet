﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CustomerGet.Service.DataFactories;
using CustomerGet.Service.Services;
using CustomerGet.Service.Apis;
using Moq;
using System.IO;
using Newtonsoft.Json;
using CustomerGet.Common.Models;
using System.Collections.Generic;

namespace CustomerGet.Service.Tests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestInitialize]
        public void Setup()
        {

        }

        //[TestMethod]
        public void LiveIntergrationTest_GetCustomersReturnsObject()
        {
            var customerDataFactory = new ShelteredDepthsDataFactory(new ShelteredDepthsApi());
            var customerService = new CustomerService(customerDataFactory);
            var resultJson = customerService.GetCustomers();

            //Has Completed
            Assert.IsNotNull(resultJson, "Json incorrectly returns null for valid API response");
            //Has Content
            Assert.IsTrue(resultJson.Length > 0, "Json incorrectly returns empty for valid API response");
        }

        //[TestMethod]
        public void LiveIntergrationTest_GetCustomerAllReturnsObject()
        {
            //Run
            var id = new Guid("be1d35af-6020-4445-9451-f13facfcfc9a");
            var customerDataFactory = new ShelteredDepthsDataFactory(new ShelteredDepthsApi());
            var customerService = new CustomerService(customerDataFactory);
            var resultJson = customerService.GetCustomer(id);

            //Has Completed
            Assert.IsNotNull(resultJson, "Json incorrectly returns null for valid API response");
            //Has Content
            Assert.IsTrue(resultJson.Length > 0, "Json incorrectly returns empty for valid API response");
        }

        [TestMethod]
        public void GetCustomersReturnsObject()
        {
            //Setup
            var customersJson = File.ReadAllText("Json/customers.json");
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetCustomersAsync())
                .Returns(Task.FromResult(customersJson));

            //Run
            var customerDataFactory = new ShelteredDepthsDataFactory(customerApi.Object);
            var customerService = new CustomerService(customerDataFactory);
            var resultJson = customerService.GetCustomers();

            //Has Completed
            Assert.IsNotNull(resultJson, "Json incorrectly returns null for valid API response");
            //Has Content
            Assert.IsTrue(resultJson.Length > 0, "Json incorrectly returns empty for valid API response");
        }

        [TestMethod]
        public void GetCustomersWithEmptyApiResultReturnsNullObject()
        {
            //Setup
            var customersJson = string.Empty;
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetCustomersAsync())
                .Returns(Task.FromResult(customersJson));

            //Run
            var customerDataFactory = new ShelteredDepthsDataFactory(customerApi.Object);
            var customerService = new CustomerService(customerDataFactory);
            var resultJson = customerService.GetCustomers();

            //Has Completed
            Assert.IsNotNull(resultJson, "Json incorrectly returns null on Empty API response");
            //Is an empty Customers object
            var nullCustomersString = JsonConvert.SerializeObject(new Customers() { ListOfCustomers = new List<Customer>() });
            Assert.IsTrue(resultJson == nullCustomersString, "Json is not an empty Customers response");
        }

        [TestMethod]
        public void GetCustomerAllReturnsObject()
        {
            //Setup
            var id = new Guid("be1d35af-6020-4445-9451-f13facfcfc9a");
            var customerJson = File.ReadAllText("Json/customer.json");
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetCustomerAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customerJson));

            //Run
            var customerDataFactory = new ShelteredDepthsDataFactory(customerApi.Object);
            var customerService = new CustomerService(customerDataFactory);
            var resultJson = customerService.GetCustomer(id);

            //Has Completed
            Assert.IsNotNull(resultJson, "Json incorrectly returns null for valid API response");
            //Has Content
            Assert.IsTrue(resultJson.Length > 0, "Json incorrectly returns empty for valid API response");
        }

        [TestMethod]
        public void GetCustomerWithEmptyApiResultReturnsNullObject()
        {
            //Setup
            var customersJson = string.Empty;
            var customerApi = new Mock<ICustomerServiceApi>();
            customerApi.Setup(arg => arg.GetCustomerAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customersJson));

            //Run
            var customerDataFactory = new ShelteredDepthsDataFactory(customerApi.Object);
            var customerService = new CustomerService(customerDataFactory);
            var resultJson = customerService.GetCustomer(new Guid());

            //Has Completed
            Assert.IsNotNull(resultJson, "Json incorrectly returns null on Empty API response");
            //Is an empty Customers object
            var nullCustomerString = JsonConvert.SerializeObject(new Customer());
            Assert.IsTrue(resultJson == nullCustomerString, "Json is not an empty Customers response");
        }
    }
}
