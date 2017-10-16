using CustomerGet.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using CustomerGet.Common.Models;

namespace CustomerGet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel();

            var customers = Business.Functions.GetAllCustomers.Get();

            model.Customers = customers.ToList();
            //model.Customers.Add(new Customer() { id=new Guid(), FirstName = "Simon1" });
            //model.Customers.Add(new Customer() { id = new Guid(), FirstName = "Simon2" });

            return View(model);
        }

        public ActionResult Customer(string CustomerId)
        {
            var model = new CustomerModel()
            {
                Customer = new Customer()
            };

            Guid idGuid;
            if (!Guid.TryParse(CustomerId, out idGuid))
                return View(model);

            var customer = Business.Functions.GetCustomer.Get(idGuid);
            model.Customer = customer;

            return View(model);
        }
    }
}