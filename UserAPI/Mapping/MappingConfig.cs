using AutoMapper;
using UserAPI.DTOs;
using UserAPI.Models;

namespace UserAPI.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappings = new MapperConfiguration(config =>
            config.CreateMap<UserDTO, User>().ReverseMap()
            );

            return mappings;
        }
    }
}
