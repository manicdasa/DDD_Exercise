using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface INotificationService
    {
        PagedList<NotificationDTO> GetUserNotifications(int userId, LookupInputModel model);
        Task<int> SendNotificationToReceivers(NotificationDTO notification, List<ApplicationUser> receivers, CancellationToken cancellationToken);
        Task<int> SendNotificationToReceivers(List<NotificationSignalRDTO> notificationDTOs, CancellationToken cancellationToken);
        Task<List<NotificationSignalRDTO>> SendNotifications(CancellationToken cancellationToken, int headProposalId, string message, string link, NotificationType notificationType, bool notifyAdmin = false, string adminMessage = "", params int[] receiverIds);
        Task<OutputModel> MarkNotificationsAsSeen(NotificationType notificationType, string username, CancellationToken cancellationToken);
        void AddSidePanelNotifications(ref List<NotificationSignalRDTO> notifications, object objectToMap, EventType eventType, PanelTab panelTab, params int[] additionalReceiverIds);
        List<NotificationSignalRDTO> AddSidePanelNotifications(object objectToMap, EventType eventType, PanelTab panelTab, params int[] additionalReceiverIds);
    }
}
