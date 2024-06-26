using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Models;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap se utiliza para definir mapeos de una clase a otra
            CreateMap<UserRegistrationDto, User>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));

            // Mapeo de DataFile a CreateFilesDto (de la entidad al DTO)
            CreateMap<CreateFilesDto, DataFile>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
        }
    }
}