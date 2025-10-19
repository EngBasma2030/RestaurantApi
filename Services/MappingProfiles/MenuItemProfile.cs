using AutoMapper;
using Domain.Models;
using Shared.DataTransferObject.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            // من Entity إلى DTO
            CreateMap<MenuItem, MenuItemDto>();

            // من DTO إلى Entity
            CreateMap<CreateMenuItemDto, MenuItem>();
            CreateMap<UpdateMenuItemDto, MenuItem>();
        }
    }
}
