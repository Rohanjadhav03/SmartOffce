using BackendApi.Models;
using BackendApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _Service;
        public AuthController(IAuthService Service)
        {
            _Service = Service;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest("Invalid login request");
                }

                LoginResponse response = await _Service.LoginAsync(request);
                if (response==null)
                {
                    return Unauthorized("Invalid username or password");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in AuthController Login: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }


        }
    }
}
