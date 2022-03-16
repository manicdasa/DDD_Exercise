using AutoMapper;
using System;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class MessageDTO : IMapFrom<Message>
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int HeadProposalId { get; set; }
        public int BookingId { get; set; }
        public string Username { get; set; }
        public bool MyMessage { get; set; }
        public bool IsLogMessage { get; set; }
        public string MessageText { get; set; }
        public DateTime DateTimeSent { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageDTO>()
              .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.SentByUser.UserName))
              .ForMember(dest => dest.HeadProposalId, opt => opt.MapFrom(s => s.Conversation.HeadProposalId))
              .ForMember(dest => dest.MyMessage, opt => opt.MapFrom(s => false));
        }
    }
}
