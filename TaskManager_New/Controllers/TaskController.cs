using Microsoft.AspNetCore.Mvc;
using TaskManager_New.DTOs;
using TaskManager_New.Services;


namespace TaskManager_New.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;
        private readonly ILogger <TaskController> _logger;

        public TaskController(ITaskServices taskServices, ILogger <TaskController> logger)
        {
            _taskServices = taskServices;
            _logger = logger;
        }

        /// <summary>
        /// Получение всех задач
        /// </summary>
        [HttpGet]
        [Route("Tasks/GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var allTasks = await _taskServices.GetAllTasks();
                return Ok(allTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех задач");
                return StatusCode(500, "Внутренняя ошибка сервера при получение всех задач.");
            }
        }


        /// <summary>
        /// Получение задачи по ее названию
        /// </summary>
        /// <param name="title">Название задачи</param>
        [HttpGet]
        [Route("Tasks/GetTaskByTitle")]
        public async Task<IActionResult> GetTaskByName([FromQuery] string title)
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
                _logger.LogError(ex, $"Ошибка получения задачи {title}");
                return StatusCode(500, "Внутренняя ошибка сервера при получении задачи.");
            }
        }


        /// <summary>
        /// Создание новой задачи для пользователя
        /// </summary>
        /// <param name="model">Модель задачи</param>
        [HttpPost]
        [Route("Tasks/CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskApiModel model)
        {
            try
            {
                var task = await _taskServices.CreateTask(model.Title, model.Description, model.UserId);
                 return Ok(task);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при попытке создания задачи {model.Title}");
                return StatusCode(500, "Внутренняя ошибка сервера при создании задачи.");
            }
        }


        /// <summary>
        /// Удаление задачи по названию для конкретного пользователя
        /// </summary>
        /// <param name="title">Название удаляемой задачи</param>
        /// <param name="userId">ID владельца задачи</param>
        [HttpDelete]
        [Route("Tasks/DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromQuery] string title, [FromQuery] int userId)
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
                _logger.LogError(ex, $"Ошибка при попытке удаления задачи {title}");
                return StatusCode(500, $"Ошибка при удалении задачи {title}");
            }
        }
        
    }
}
