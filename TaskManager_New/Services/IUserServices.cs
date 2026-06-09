using TaskManager_New.DTOs;
using TaskManager_New.Models;

namespace TaskManager_New.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        Task<IEnumerable<UserResponse>> GetAllUsers();

        /// <summary>
        /// Создание пользователя
        /// </summary>
        Task<string> CreateUser(UserApiModel model);

        /// <summary>
        /// Удаление пользователя с его задачами
        /// </summary>
        Task<bool> DeleteUser(string userLogin);
    }
}
