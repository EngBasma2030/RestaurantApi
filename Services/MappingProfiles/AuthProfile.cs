using AutoMapper;
using Domain.Models;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));
            CreateMap<User, AuthResponseDto>();
        }
    }
}
