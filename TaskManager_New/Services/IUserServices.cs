using TaskManager_New.DTOs;
using TaskManager_New.Models;

namespace TaskManager_New.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        Task<IEnumerable<User>> GetAllUsers();

        /// <summary>
        /// Создание пользователя
        /// </summary>
        Task<User> CreateUser(UserApiModel model);
    }
}
