using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.OrderBy(x => x.Id).ToListAsync();
        }


        /// <summary>
        /// Создание пользователя
        /// </summary>
        public async Task<User> CreateUser(UserApiModel model)
        {
            try
            {
                var user = new User
                {
                    Name = model.Name,
                    Login = model.Login,
                    Password = model.Password,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
            {
                throw new Exception($"Пользователь с логином '{model.Login}' уже существует");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при создании пользователя: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаление пользователя с его задачами
        /// </summary>
        public async Task<bool> DeleteUser(string userLogin)
        {
                var idUser = await _context.Users
                    .Where(a => a.Login == userLogin)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                if(idUser != 0) {
                    var userToDelete = await _context.Users.FindAsync(idUser);
                    if(userToDelete != null)
                    {
                        var taskToDelete = await _context.TaskItems
                        .Where(u => u.UserId == idUser)
                        .ToListAsync();

                        _context.Users.Remove(userToDelete);
                        _context.TaskItems.RemoveRange(taskToDelete);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
        }



    }
}
