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

        public TaskServices(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Получение всех задач
        /// </summary>
        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            return await _context.TaskItems.OrderBy(p => p.Id).ToListAsync();
        }

        /// <summary>
        /// Получение задачи по названию
        /// </summary>
        /// /// <param name="tiitle">Название задачи</param>
        public async Task<IEnumerable<TaskItem>> GetTasksByTitle(string title)
        {
            return await _context.TaskItems
                .Where(a => a.Title == title)
                .ToListAsync();
        }

        /// <summary>
        /// Получение задач пользователя
        /// </summary>
        public async Task<List<TaskItem>> GetUserTasks(int id)
        {
            return await _context.TaskItems
                .Where(t => t.UserId == id)  
                .ToListAsync();
        }


        /// <summary>
        /// Получение задачи по Id
        /// </summary>
        /// <param name="id">id задачи</param> 
        public async Task<TaskItem?> GetTaskById(int id)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(a => a.Id == id);
        }


        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="title">Название задачи</param>
        /// <param name="description">Описание задачи</param>
        /// <param name="userId">id пользователя</param>
        public async Task<TaskItem> CreateTask(string title, string description, int userId)
        {
            try
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
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
            {
                throw new InvalidOperationException($"У пользователя с ID '{userId}' уже есть задача с названием '{title}'");
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23503")
            {
                throw new InvalidOperationException($"Пользователь с ID '{userId}' не существует");
            }
        }


        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="title"></param>
        /// <param name="userId">id пользователя</param>
        public async Task<bool> DeleteTask(string title, int userId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(a => a.Title == title && a.UserId == userId);
            if (task == null)
                return false;

            _context.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    
}
}
