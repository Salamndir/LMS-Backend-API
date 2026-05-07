using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Dependency Injection of the Auth Service
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Call the service to perform business logic
            var token = _authService.Login(request);

            // If service returned null, it means authentication failed
            if (token == null)
                return Unauthorized("Invalid credentials");

            // If successful, return 200 OK with the token
            return Ok(new { token = token });
        }
    }
}