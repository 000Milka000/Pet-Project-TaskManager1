using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using TaskManager_New.Data;
using TaskManager_New.DTOs;
using TaskManager_New.Models;



namespace TaskManager_New.Services.Users
{
    public class UserServices : IUserServices
    {
        public readonly ApplicationDbContext _context;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            return await _context.Users
                .OrderBy(x => x.Id)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Login = u.Login,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }


        /// <summary>
        /// Создание пользователя
        /// </summary>
        public async Task<string> CreateUser(UserApiModel model)
        {
                var hasUser = await _context.Users
                    .FirstOrDefaultAsync(a => a.Login == model.Login);
                if (hasUser != null)
                {
                    throw new InvalidOperationException($"Пользователь с логином '{model.Login}' уже существует");
                }

                var user = new User
                {
                    Name = model.Name,
                    Login = model.Login,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    CreatedAt = DateTime.UtcNow
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                var result = $"Пользователь {user.Name} создан. Логин: {user.Login}. ID: {user.Id}.";
                return result;
            }


        /// <summary>
        /// Удаление пользователя с его задачами
        /// </summary>
        public async Task<bool> DeleteUser(string userLogin)
        {
            var userToDelete = await _context.Users
                .FirstOrDefaultAsync(a => a.Login == userLogin);
            if (userToDelete != null)
            {
                var taskToDelete = await _context.TaskItems
                        .Where(u => u.UserId == userToDelete.Id)
                        .ToListAsync();

                _context.Users.Remove(userToDelete);
                _context.TaskItems.RemoveRange(taskToDelete);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        }
}
