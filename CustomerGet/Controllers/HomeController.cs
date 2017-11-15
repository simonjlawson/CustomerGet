using CustomerGet.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using CustomerGet.Common.Models;
using CustomerGet.Business.Functions;

namespace CustomerGet.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerDataFactory CustomerDataFactory;

        public HomeController(ICustomerDataFactory customerDataFactory)
        {
            CustomerDataFactory = customerDataFactory;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            var customers = CustomerDataFactory.GetAll();
            model.Customers = customers.ListOfCustomers.ToList();

            return View(model);
        }

        public ActionResult Customer(string CustomerId)
        {
            var model = new CustomerViewModel()
            {
                Customer = new Customer()
            };

            Guid idGuid;
            if (!Guid.TryParse(CustomerId, out idGuid))
                return View(model);

            var customer = CustomerDataFactory.Get(idGuid);
            model.Customer = customer;

            return View(model);
        }

        public ActionResult Save(string customerId, string txtFirstname)
        {
            var customerModel = new CustomerViewModel();
            customerModel.Customer = new Common.Models.Customer();
            customerModel.Customer.id = customerId;
            customerModel.Customer.FirstName = txtFirstname;
                CustomerDataFactory.Save(customerModel.Customer);
            
            return Index();
        }
    }
    }