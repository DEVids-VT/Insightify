using AutoMapper;
using Insightify.Framework.Messaging.Events;
using Insightify.NotificationsAPI.Constants;
using Insightify.NotificationsAPI.Models;

namespace Insightify.NotificationsAPI.Profiles
{
    public class NotificationMapping : Profile
    {
        public NotificationMapping()
        {
            CreateMap<NotificationEvent, Notification>();
        }
    }
}
