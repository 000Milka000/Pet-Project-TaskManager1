using Microsoft.AspNetCore.Mvc;
using TaskManager_New.DTOs;
using TaskManager_New.Services;


namespace TaskManager_New.Controllers
{
    [ApiController]
    public class TaskControllers : ControllerBase
    {
        private readonly ITaskServices _taskServices;

        public TaskControllers(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [HttpGet]
        [Route("Tasks/GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var allTasks = await _taskServices.GetAllTask();
                return Ok(allTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet]
        [Route("Tasks/GetTaskByTitle")]
        public async Task<IActionResult> GetTaskByName(string title)
        {
            try
            {
                var taskByName = await _taskServices.GetTaskByTitle(title);
                if (taskByName == null)
                {
                    return NotFound($"Задача с названием '{title}' не найдена");
                }
                return Ok(taskByName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost]
        [Route("Tasks/CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskApiModel model)
        {
            try
            {
                var task = await _taskServices.CreateTask(model.Title, model.Description, model.UserId);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Tasks/DeleteTask")]
        public async Task<IActionResult> DeleteTask(string title, int userId)
        {
            try
            {
                var result = await _taskServices.DeleteTask(title, userId);
                if (!result)
                {
                    return NotFound($"Задача с названием '{title}' для пользователя {userId} не найдена");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
