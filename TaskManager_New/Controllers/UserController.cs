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

        public UserController(IUserServices userSrevices, ITaskServices taskServices)
        {
            _userSrevices = userSrevices;
            _taskServices = taskServices;
        }


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
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet]
        [Route("User/GetTasksByUser")]
        public async Task<IActionResult> GetTasksByUser(int id)
        {
            try
            {
                var tasks = await _taskServices.GetUserTasks(id);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }

        }

        [HttpPost]
        [Route("User/CreateUser")]
        public async Task<IActionResult> CreateUser(UserApiModel model)
        {
            try
            {
                var user = await _userSrevices.CreateUser(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
