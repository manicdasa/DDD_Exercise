using System;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class NotificationDTO : IMapFrom<Domain.Entities.Notification>
    {
        public string Message { get; set; }
        public string DetailsLink { get; set; }
        public int HeadProposalId { get; set; }
        public int ProjectId { get; set; }
        public int ExactEntityId { get; set; }
        public string ProjectTopic { get; set; }
        public string AuthorUsername { get; set; }
        public string CustomerUsername { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool IsSeen { get; set; }
        public NotificationType NotificationType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Notification, NotificationDTO>()
                .ForMember(d => d.ExactEntityId, opt => opt.MapFrom(s => s.ProposalId));
        }
    }

    public class NotificationSignalRDTO
    {
        public int ReceiverUserId { get; set; }
        public NotificationDTO NotificationDTO { get; set; }
        public PanelObjectDTO PanelObjectDTO { get; set; }
    }

    public class PanelObjectDTO
    {
        public EventType EventType { get; set; }
        public PanelTab PanelTab { get; set; }
        public object PanelObject { get; set; }
    }

    public enum EventType
    {
        Create = 0,
        Delete = 1,
        Change = 2
    }

    public enum PanelTab
    {
        Offer = 0,
        Bid = 1,
        Chat = 2
    }
}
