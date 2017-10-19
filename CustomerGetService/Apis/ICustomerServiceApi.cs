using System;
using System.Threading.Tasks;

namespace CustomerGet.Service.Apis
{
    public interface ICustomerServiceApi
    {
        Task<string> GetCustomersAsync();

        Task<string> GetCustomerAsync(Guid id);
    }
}
