using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFirst.Models.DTOs;
using MyFirst.Models.Entities;
using MyFirst.Repository.Repository.Contracts;
using MyFirst.Services.Services.Contracts;
using System.Threading.Tasks;

namespace MyFirst.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentsController(IRepository<Student> studentRepository, IMapper mapper, IStudentService studentService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _studentService.GetAllStudentsAsync());
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentService.GetAsync(id.Value);
            if (student == null)
                return NotFound();

            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

            await _studentService.AddAsync(student);

            return Ok(student.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromBody] StudentDto studentDto)
        {
            if (id == null)
                return NotFound();

            if (id != studentDto.Id)
                return BadRequest();

            var existStudent = await _studentService.GetAsync(id.Value);
            if (existStudent == null)
                return NotFound();

            var student = _mapper.Map<Student>(studentDto);

            await _studentRepository.UpdateAsync(student);

            return Ok();
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentService.GetAsync(id.Value);
            if (student == null)
                return NotFound();

            await _studentRepository.DeleteAsync(student);

            return NoContent();
        }
    }
}
