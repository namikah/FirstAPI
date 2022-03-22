using Microsoft.AspNetCore.Mvc;
using MyFirst.AuthenticationService.Contracts;
using MyFirst.AuthenticationService.Models;
using System.Threading.Tasks;

namespace MyFirst.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] CredentialModel credentialModel)
        {
            var token =await _authService.GetTokenAsync(credentialModel);

            return Ok(token);
        }
    }
}
