using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backeng.Models;

namespace Backeng.Profiles
{
    public class AutoMapperProfile
    {
        CreateMap<UserRegistrationDto, User>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));
    }
}