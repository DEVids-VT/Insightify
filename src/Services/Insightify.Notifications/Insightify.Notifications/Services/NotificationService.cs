using FluentValidation;
using Insightify.Framework.DependencyInjection.Attributes;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.NotificationsAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace Insightify.NotificationsAPI.Services
{
    [Register(ServiceLifetime.Scoped)]
    public class NotificationService : INotificationService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Notification> _repo;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IValidator<Notification> _validator;

        public NotificationService(ILogger<NotificationService> logger, IRepository<Notification> repo, IHubContext<NotificationHub> hubContext, IValidator<Notification> validator)
        {
            _logger = logger;
            _repo = repo;
            _hubContext = hubContext;
            _validator = validator;
        }

        public async Task StoreNotification(Notification notification)
        {
            _logger.LogInformation("Storing notification {0}", notification.Title);

            await _repo.InsertAsync(notification);
        }

        public async Task SendNotificaiton(Notification notification)
        {
            _logger.LogInformation("Sending notification {0}", notification.Title);

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task<Notification> GetByIdAsync(string id, bool includeDeleted = false)
        {
            _logger.LogInformation("Receiving the notification with id {0}", id);

            var notification = await _repo.GetFirstOrDefaultAsync(x => x.Id == id, includeDeleted: includeDeleted);

            return notification;
        }

        public async Task<IPagedList<Notification>> GetAllAsync(int pageNumber, int pageSize = 10)
        {
            var articles = await _repo.GetPagedListAsync(pageNumber, pageSize);

            if (articles == null || articles.TotalCount == 0)
            {
                return null;
            }

            return articles;
        }
    }
}
