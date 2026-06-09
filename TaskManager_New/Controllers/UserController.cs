using Microsoft.AspNetCore.Mvc;
using TaskManager_New.DTOs;
using TaskManager_New.Services;
using TaskManager_New.Services.Users;

namespace TaskManager_New.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userSrevices;
        private readonly ITaskServices _taskServices;
        private readonly ILogger <UserController> _logger;

        public UserController(IUserServices userSrevices, ITaskServices taskServices, ILogger<UserController> logger)
        {
            _userSrevices = userSrevices;
            _taskServices = taskServices;
            _logger = logger;
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        [HttpGet]
        [Route("User/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _userSrevices.GetAllUsers();
                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении списка пользователей.");
                return StatusCode(500, $"Внутренняя ошибка сервера при получении всех пользователей.");
            }
        }

        /// <summary>
        /// Получение всех задач, назначенных на конкретного пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        [HttpGet]
        [Route("User/GetTasksByUser")]
        public async Task<IActionResult> GetTasksByUser([FromQuery] int id)
        {
            try
            {
                var tasks = await _taskServices.GetUserTasks(id);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Внутренняя ошибка сервера при получении задач пользователя. Ошибка: {ex.Message}");
            }

        }


        /// <summary>
        /// Создание нового пользователя 
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        [HttpPost]
        [Route("User/CreateUser")]
        public async Task<IActionResult> CreateUser(UserApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Некорректный запрос");
            }
            try
            {
                var user = await _userSrevices.CreateUser(model);
                return Ok(user);
            }
            catch (InvalidOperationException ex) 
            {
                return Conflict(new { error = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Внутренняя ошибка сервера при создании пользователя.");
            }
            
        }

        /// <summary>
        /// Удаление пользователя и всех его задач
        /// </summary>
        /// <param name="userLogin">Логин удаляемого пользователя</param>
        [HttpDelete]
        [Route("User/DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string? userLogin)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(userLogin))
                {
                    bool deleted = await _userSrevices.DeleteUser(userLogin);
                    if(deleted)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound($"Пользователь с логином '{userLogin}' не найден.");
                    }
                }
                else
                {
                    return BadRequest($"Не введен логин пользователя.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Внутренняя ошибка сервера при удалении пользователя");
            }
        }
    }
}
