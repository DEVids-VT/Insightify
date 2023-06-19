using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.NotificationsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.NotificationsAPI.Services
{
    public interface INotificationService
    {
        Task StoreNotification(Notification notification);
        Task SendNotificaiton(Notification notification);
        Task<Notification> GetByIdAsync(string id, bool includeDeleted = false);
        Task<IPagedList<Notification>> GetAllAsync(int pageNumber, int pageSize = 10);
    }
}
