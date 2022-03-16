using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Enums;
using System.Threading;
using GhostWriter.Application.DTOs;
using System.Collections.Generic;
using GhostWriter.Application.Common.Models.Shared;
using System;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Customer,Ghostwriter")]
    public class NotificationController : ApiControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Marks group of notifications as seen
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(MarkNotificationsAsSeen))]
        public async Task<OutputModel> MarkNotificationsAsSeen(NotificationType notificationType, CancellationToken cancellationToken)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            var result = await _notificationService.MarkNotificationsAsSeen(notificationType, username, cancellationToken);
            if (result.Success)
                return result;
            else
                throw new Exception(result.Message);
        }

        /// <summary>
        /// Returns user notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetUserNotifications))]
        public PagedList<NotificationDTO> GetUserNotifications([FromQuery] LookupInputModel dtRequest)
        {
            int userId;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);

            var result = _notificationService.GetUserNotifications(userId, dtRequest);

            return result;
        }
    }
}
