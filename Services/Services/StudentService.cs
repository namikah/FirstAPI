using AutoMapper;
using Models.DTOs;
using Models.Entities;
using Repository.DataContext;
using Repository.Repository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
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
