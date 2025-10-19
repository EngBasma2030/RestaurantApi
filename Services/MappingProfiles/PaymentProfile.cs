using AutoMapper;
using Domain.Models;
using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<CreatePaymentDto, Payment>();
        }
    }
}
