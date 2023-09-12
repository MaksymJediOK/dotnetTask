using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserTest.Infrastructure.Dtos;
using UserTest.Infrastructure.Services;

namespace UserTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }
        /// <summary>
        /// Registers a new user using Create dto
        /// </summary> 
        /// /// <param name="userCreateDto">Dto</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            var result = await _jwtService.Register(userCreateDto);

            return Ok(result);
        }
        /// <summary>
        /// Return jwt token after verifying credentials
        /// </summary> 
        /// /// <param name="userLoginDto">Dto</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var token = await _jwtService.Login(userLoginDto);
            return Ok(token);
        }

    }
}
