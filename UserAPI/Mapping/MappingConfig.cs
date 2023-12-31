﻿using AutoMapper;
using UserAPI.DTOs;
using UserAPI.Models;

namespace UserAPI.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappings = new MapperConfiguration(config =>
            {
                config.CreateMap<UserDTO, User>();

                config.CreateMap<User, UserDTO>()
                .ForMember(src => src.Hash, dest => dest.Ignore())
                .ForMember(src => src.Salt, dest => dest.Ignore());
            });

            return mappings;
        }
    }
}
