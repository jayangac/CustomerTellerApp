using AutoMapper;
using CutomerTeller.WebAPIApp.Data;
using CutomerTeller.WebAPIApp.Model.Customer;
using System;

namespace CutomerTeller.WebAPIApp.Core.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, Customer>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
