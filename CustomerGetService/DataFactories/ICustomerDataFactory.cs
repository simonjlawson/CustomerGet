using CustomerGet.Common.Models;
using System;
using System.Collections.Generic;

namespace CustomerGet.Service.DataFactories
{
    public interface ICustomerDataFactory
    {
        Customers GetCustomers();

        Customer GetCustomer(Guid id);

        bool PutCustomer(Guid id, string firstname);
    }
}
