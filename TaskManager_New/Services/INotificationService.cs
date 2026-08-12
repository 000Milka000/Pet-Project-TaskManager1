using TaskManager.Models;

namespace TaskManager_New.Services
{
    public interface INotificationService
    {
        Task<string> SendNotificationAsync(string recipient, string message);
    }
}
