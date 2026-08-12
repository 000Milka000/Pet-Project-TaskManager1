using TaskManager_New.Models;

namespace TaskManager_New.Services
{
    public interface ITaskServices
    {
        /// <summary>
        /// Получение всех задач
        /// </summary>
        Task<IEnumerable<TaskItem>> GetAllTasks();


        /// <summary>
        /// Получение задачи по названию
        /// </summary>
        Task<IEnumerable<TaskItem>> GetTasksByTitle(string title);

        /// <summary>
        /// Получение задач пользователя
        /// </summary>
        Task<List<TaskItem>> GetUserTasks(int id);

        /// <summary>
        /// Получение задачи по Id
        /// </summary>
        Task<TaskItem?> GetTaskById(int  id);

        /// <summary>
        /// Создание задачи
        /// </summary>
        Task<TaskItem> CreateTask(string title, string description, int userId);

        /// <summary>
        /// Удаление задачи
        /// </summary>
        Task<bool> DeleteTask(string title, int userId);
    }
}
