using Microsoft.AspNetCore.Mvc;
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
            authService = _authService;
        }

        /// <summary>
        /// Создание JWT токена
        /// </summary>
        [HttpPost]
        [Route("Auth/Token")]
        public async Task<IActionResult> GeneratieToken([FromBody] TokenRequestDto request)
        {
            try
            {
                var result = _authService.GeneratieToken(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }


}
