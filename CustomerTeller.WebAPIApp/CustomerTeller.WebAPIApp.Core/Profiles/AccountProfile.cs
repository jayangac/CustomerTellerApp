using AutoMapper;
using CutomerTeller.WebAPIApp.Data;
using CutomerTeller.WebAPIApp.Model;
using System;

namespace CutomerTeller.WebAPIApp.Core.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountModel, Account>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
