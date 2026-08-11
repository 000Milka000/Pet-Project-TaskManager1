using TaskManager_New.DTOs;
using TaskManager.Models;

namespace TaskManager_New.Services
{
    public interface IAuthService
    {
        //Task<AuthResponse> Login (AuthRequest request);
        //Task<AuthResponse> Registration (AuthRequest request);
        //Task<bool> Logout(int id);
        //Task<bool> ChangePassword(int iserId, string oldPasword, string newPassword);
        //Task<bool> ValidateToken(string token);
        //Task<User> GetUserByToken(string token);
        Task<string> GenerateToken(string login, string password);
    }
}
