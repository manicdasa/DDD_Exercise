using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Defaults;

namespace GhostWriter.WebUI.Hubs
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        private readonly INotificationService _notificationService;

        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                if (Context.User is null || Context.User.FindFirst(ClaimTypes.NameIdentifier) is null || !int.TryParse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userid))
                    return base.OnConnectedAsync();

                Groups.AddToGroupAsync(Context.ConnectionId, userid.ToString());

                var notifications = _notificationService.GetUserNotifications(userid, new Application.Common.Models.Shared.LookupInputModel() { PageSize = 20 });

                notifications.Items.ForEach(x => Clients.Group(userid.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x));
                
                return base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                return base.OnConnectedAsync();
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
