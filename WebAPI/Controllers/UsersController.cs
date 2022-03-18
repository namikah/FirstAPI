using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IUserRepository<User> repository, IMapper mapper, IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            if (id == null)
                return NotFound();

            var user = await _userService.GetAsync(id);
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userService.AddAsync(user);

            return Ok(user.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UserDto userDto)
        {
            if (id == null)
                return NotFound();

            var existUser = await _userService.GetAsync(id);
            if (existUser == null)
                return NotFound();

            var user = _mapper.Map<User>(userDto);

            await _userService.UpdateAsync(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (id == null)
                return NotFound();

            var user = await _userService.GetAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(user);

            return NoContent();
        }
    }
}
