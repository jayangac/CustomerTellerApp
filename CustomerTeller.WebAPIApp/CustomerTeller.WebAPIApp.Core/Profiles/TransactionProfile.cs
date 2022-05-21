using AutoMapper;
using CutomerTeller.WebAPIApp.Data.EntityModels;
using CutomerTeller.WebAPIApp.Model.Transaction;
using System;

namespace CutomerTeller.WebAPIApp.Core.Profiles
{
    public  class TransactionProfile :Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionModel, Transaction>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
