using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager_New.Data;
using TaskManager_New.DTOs;
using TaskManager_New.Models;
using TaskManager_New.Services;
using TaskManager_New.Services.Task;

namespace TaskManager_New.Controllers
{
    [ApiController]
    public class TaskControllers : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITaskServices _taskServices;

        public TaskControllers(ApplicationDbContext context, ITaskServices taskServices)
        {
            _context = context;
            _taskServices = taskServices;
        }

        [HttpGet]
        [Route("Tasks/GetAllTasks")]
        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            var allTasks = await _taskServices.GetAllTask();
            return allTasks;
        }

        [HttpGet]
        [Route("Tasks/GetTaskByTitle")]
        public async Task<TaskItem?> GetTaskByName(string title)
        {
            var taskByName = await _taskServices.GetTaskByTitle(title);
            return taskByName;
        }

        [HttpPost]
        [Route("Tasks/CreateTask")]
        public async Task<TaskItem> CreateTask([FromBody] TaskApiModel model)
        {
            try
            {
                var task = await _taskServices.CreateTask(model.Title, model.Description, model.UserId);
                return task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        [HttpDelete]
        [Route("Tasks/DeleteTask")]
        public async Task<bool> DeleteTask(string title, int userId)
        {
            var result = await _taskServices.DeleteTask(title, userId);
            return result;
        }
        
    }
}
