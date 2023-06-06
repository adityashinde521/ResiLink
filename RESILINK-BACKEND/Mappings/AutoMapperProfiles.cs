using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace HSPA_BACKEND.Mappings
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles() { 
        
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, UpdateCityRequestDto>().ReverseMap();
            CreateMap<City, UpdateCityRequestDto>().ReverseMap();

            CreateMap<FurnishingType, FurnishingTypeDto>().ReverseMap();
            CreateMap<PropertyType, PropertyTypeDto>().ReverseMap();

            CreateMap<Property, PropertyListingDto>().ReverseMap();
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

        }


    }
}
