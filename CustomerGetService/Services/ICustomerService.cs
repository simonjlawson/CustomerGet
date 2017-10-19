using CustomerGet.Common.Models;
using System;
using System.Collections.Generic;

namespace CustomerGet.Service.Services
{
    public interface ICustomerService
    {
        string GetCustomers(int page);

        string GetCustomer(Guid id);
    }
}
