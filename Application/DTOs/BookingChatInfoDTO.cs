using System;
using System.Linq;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class BookingChatInfoDTO : IMapFrom<Domain.Entities.Booking>
    {
        public int BookingId { get; set; }
        public int HeadProposalId { get; set; }
        public string AuthorUsername { get; set; }
        public int AuthorId { get; set; }
        public string CustomerUsername { get; set; }
        public BookingStatusDTO BookingStatus { get; set; }
        public string ProjectTopic { get; set; }
        public string LastMessageContent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Booking, BookingChatInfoDTO>()
                .ForMember(d => d.BookingId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.HeadProposalId, opt => opt.MapFrom(s => s.HeadProposal.Id))
                .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.Id))
                .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.HeadProposal.Project.Customer.UserName))
                .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic))
                .ForMember(d => d.BookingStatus, opt => opt.MapFrom(s => (s.BookingStatusHistories != null && s.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault() != null) ? BookingStatusDescriptionValues.GetDescription(s.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus) : new BookingStatusDTO() { Id = 0, Value = string.Empty}))
                .ForMember(d => d.LastMessageContent, opt => opt.MapFrom(s => s.HeadProposal.Conversation.Messages.FirstOrDefault() != null ? s.HeadProposal.Conversation.Messages.OrderByDescending(x => x.DateTimeSent).FirstOrDefault().MessageText : string.Empty));
        }
    }
}
