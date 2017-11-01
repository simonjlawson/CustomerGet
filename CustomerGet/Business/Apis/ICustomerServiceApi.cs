using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Configuration;
using CustomerGet.Common.Models;

namespace CustomerGet.Business.Functions
{
    public interface ICustomerServiceApi
    {
        Task<string> GetAllAsync();

        Task<string> GetAsync(Guid id);

        bool PutAsync(Customer customer);
    }
}