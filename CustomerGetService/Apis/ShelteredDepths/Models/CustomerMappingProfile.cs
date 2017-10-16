using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CustomerGet.Service.Apis.ShelteredDepths.Models
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customers, Common.Models.Customers>();
            CreateMap<Customer, Common.Models.Customer>();
        }
    }
}
