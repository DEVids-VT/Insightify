using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using MassTransit;
using Insightify.NotificationsAPI.Events;
using AutoMapper;
using Insightify.NotificationsAPI.Models;
using Insightify.NotificationsAPI.Services;
using FluentValidation;
using Insightify.NotificationsAPI.Validators;

namespace Insightify.NotificationsAPI.Consumer
{
    public class EventConsumer : IConsumer<NotificationEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IValidator<Notification> _validator;

        public EventConsumer(IMapper mapper, ILogger<EventConsumer> logger, INotificationService notificationService, IValidator<Notification> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _notificationService = notificationService;
            _validator = validator;
        }

        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            _logger.LogInformation($"Consuming event with message Id {0}", context.Message.Id);

            var notification = _mapper.Map<Notification>(context.Message);

            if (_validator.Validate(notification).IsValid)
            {
                await _notificationService.StoreNotification(notification);

                await _notificationService.SendNotificaiton(notification);
            }
            else _logger.LogInformation($"Consumed event with message Id {0} was not valid", context.Message.Id);
        }
    }
}
