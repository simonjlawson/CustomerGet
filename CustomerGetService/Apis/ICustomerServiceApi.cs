using CustomerGet.Common.Models;
using System;
using System.Threading.Tasks;

namespace CustomerGet.Service.Apis
{
    public interface ICustomerServiceApi
    {
        Task<string> GetCustomersAsync();

        Task<string> GetCustomerAsync(Guid id);

        Task<string> PutCustomersAsync(Guid id, string firstname);
    }
}
