using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using TaskManager_New.Models;


namespace TaskManager_New.Services.Notification
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly ChannelReader<NotificationEntity> _reader;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService (ChannelReader<NotificationEntity> reader, ILogger<NotificationBackgroundService> logger){
            _reader = reader;
            _logger = logger;
        }
        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var notification in _reader.ReadAllAsync(stoppingToken))
            {
                _logger.LogInformation("Получено уведомление {Id} для {Recipient}", notification.Id, notification.Recipient);
            }
        }
    }
}
