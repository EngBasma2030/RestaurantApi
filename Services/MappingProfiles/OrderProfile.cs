using AutoMapper;
using Domain.Models;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();
        }
    }
}
