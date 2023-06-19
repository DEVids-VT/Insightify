using Insightify.NotificationsAPI.Pagination;
using Insightify.NotificationsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insightify.NotificationsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotification(string notificationId, [FromQuery] bool includeDeleted)
        {
            var result = await _notificationService.GetByIdAsync(notificationId, includeDeleted);
            
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications(
            [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
            int pageIndex = 1,
            [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
            int pageSize = 50)
        {
            var result = await _notificationService.GetAllAsync(pageIndex, pageSize);

            return result != null ? Ok(result.ToPage()) : NotFound();
        }
    }
}
