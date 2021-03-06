using AutoMapper;
using MyFirst.Models.DTOs;
using MyFirst.Models.Entities;
using MyFirst.Repository.DataContext;
using MyFirst.Repository.Repository;
using MyFirst.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirst.Services.Services
{
    public class StudentService : EFCoreRepository<Student>, IStudentService
    {
        private readonly IMapper _mapper;

        public StudentService(AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<IList<StudentDto>> GetAllStudentsAsync()
        {
            var students = await GetAllAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public object GetTest()
        {
            return "Test";
        }
    }
}
