using TaskManager_New.Models;

namespace TaskManager_New.Services
{
    public interface ITaskServices
    {
        /// <summary>
        /// Получение всех задач
        /// </summary>
        Task<IEnumerable<TaskItem>> GetAllTask();


        /// <summary>
        /// Получение задачи по названию
        /// </summary>
        Task<TaskItem?> GetTaskByName(string title);

        /// <summary>
        /// Получение задачи по Id
        /// </summary>
        Task<TaskItem?> GetTaskById(int  id);

        /// <summary>
        /// Получение задач пользователя
        /// </summary>
        Task<List<TaskItem?>> GetUserTask(int id);
    }
}
