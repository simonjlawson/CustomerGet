using CustomerGet.Common.Models;
using System;
using System.Collections.Generic;

namespace CustomerGet.Business.Functions
{
    public interface ICustomerDataFactory
    {
        Customer Get(Guid id);

        Customers GetAll();

        void Save(Customer customer)
    }
}
