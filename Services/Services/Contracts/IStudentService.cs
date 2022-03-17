using Models.DTOs;
using Models.Entities;
using Repository.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IStudentService : IRepository<Student>
    {
        object GetTest();

        Task<IList<StudentDto>> GetAllStudentsAsync();
    }
}
