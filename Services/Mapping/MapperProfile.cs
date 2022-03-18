using AutoMapper;
using MyFirst.Models.DTOs;
using MyFirst.Models.Entities;

namespace MyFirst.Services.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
