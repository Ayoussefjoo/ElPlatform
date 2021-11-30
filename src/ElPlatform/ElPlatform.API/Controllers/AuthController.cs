using ElPlatform.BAL.Exceptions;
using ElPlatform.BAL.IServices;
using ElPlatform.BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _userService;
        private IConfiguration _configuration;
        public AuthController(IAuthService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        // /api/auth/register
        [HttpPost("Register")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Some properties are not valid", ModelState.Select(m => $"{m.Key} - {m.Value}").ToArray());

            var result = await _userService.RegisterUserAsync(model);

            return Ok(new ApiResponse()
            {
                Message = "User created successfully!"
            });
        }

        // /api/auth/login
        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AccessTokenResult>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Some properties are not valid", ModelState.Select(m => $"{m.Key} - {m.Value}").ToArray());

            var result = await _userService.LoginUserAsync(model);

            return Ok(new ApiResponse<AccessTokenResult>(new AccessTokenResult(result.Message, result.ExpireDate.Value), "Access token retrieved successfully"));
        }
    }
}
