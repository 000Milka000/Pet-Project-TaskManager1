using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager_New.Data;
using TaskManager_New.Models;



namespace TaskManager_New.Services.Task
{
    public class TaskServices : ITaskServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskServices> _logger;

        public TaskServices(ApplicationDbContext context, ILogger<TaskServices> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Получение всех задач
        /// </summary>
        public async Task <IEnumerable<TaskItem>> GetAllTask()
        {
            return await _context.TaskItems.OrderBy(p => p.Id).ToListAsync();
        }

        /// <summary>
        /// Получение задачи по названию
        /// </summary>
        public async Task<TaskItem?> GetTaskByName(string title)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(a => a.Title == title);
            if (task == null)
            {
                _logger.LogWarning($"Задача '{title}' не найдена.");
            }
            return task;

        }


        /// <summary>
        /// Получение задачи по Id
        /// </summary>
        public async Task<TaskItem?> GetTaskById(int id)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(a => a.Id == id);
            if (task == null)
            {
                _logger.LogWarning($"Задача с id '{id}' не найдена.");
            }
            return task;
        }


        /// <summary>
        /// Получение задач пользователя
        /// </summary>
        public async Task<List<TaskItem?>> GetUserTask(int id)
        {
            var taskList = await _context.TaskItems.Where(a => a.UserId == id).ToListAsync();
            return taskList;
        }

    }
}
