using AutoMapper;
using Domain.Models;
using Shared.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<UpdateOrderItemDto, OrderItem>();
        }
    }
}
