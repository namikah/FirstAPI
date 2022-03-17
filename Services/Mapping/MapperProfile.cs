using AutoMapper;
using Models.DTOs;
using Models.Entities;

namespace Repository.Mapping
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
