using Npgsql.Internal;
using TaskManager_New.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;



namespace TaskManager_New.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ChannelWriter<NotificationEntity> _writer;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ChannelWriter<NotificationEntity> writer, ILogger<NotificationService> logger)
        {
            _writer = writer;
            _logger = logger;
        }



        public async Task<string> SendNotificationAsync(string? recipient, string? message)
        {

            if (string.IsNullOrEmpty(recipient) || string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Recipient и message не могут быть пустыми");
            }


            var notification = new NotificationEntity
            { 
                Id = Guid.NewGuid().GetHashCode(),
                Recipient = recipient,
                Message = message,
                CreatedAt = DateTime.UtcNow
            };

            await _writer.WriteAsync(notification);


            _logger.LogInformation(
                "Уведомление {Id} для {Recipient} поставлено в очередь",
                notification.Id,
                recipient);


            string result = "Good";
            return result;


        }
    }
}
