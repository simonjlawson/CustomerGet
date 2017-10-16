using CustomerGet.Common.Models;
using System;
using System.Collections.Generic;

namespace CustomerGet.Service.Services
{
    public interface ICustomerService
    {
        string GetCustomers();

        string GetCustomer(Guid id);
    }
}
