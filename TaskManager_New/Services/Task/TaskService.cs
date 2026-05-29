using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        public async Task<TaskItem?> GetTaskByTitle(string title)
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
        public async Task<List<TaskItem>> GetUserTask(int id)
        {
            try {
                var taskList = await _context.TaskItems.Where(a => a.UserId == id).ToListAsync();
                return taskList!;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Ошибка получения задач для пользователя {id}");
                throw;
            }
        }

        /// <summary>
        /// Создание задачи
        /// </summary>
        public async Task<TaskItem> CreateTask(string title, string description, int userId)
        {
                var task = new TaskItem
                {
                    Title = title,
                    Description = description,
                    UserId = userId
                };

                _context.Add(task);
                await _context.SaveChangesAsync();

                return task;
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        public async Task<bool> DeleteTask(string? title, int? userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(a => a.Title == title && a.UserId == userId);
            if (task == null)
            {
                return false;
            } 
            
            _context.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
