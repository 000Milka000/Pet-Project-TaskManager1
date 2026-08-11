using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager_New.DTOs;
using TaskManager_New.Services;

namespace TaskManager_New.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Создание JWT токена
        /// </summary>
        [HttpPost]
        [Route("Auth/Token")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthRequest authRequest)
        {
            try
            {
                var token = await _authService.GenerateToken(authRequest.Login, authRequest.Password);
                return Ok(new { token = token });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }


}
