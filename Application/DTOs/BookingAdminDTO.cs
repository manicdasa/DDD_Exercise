using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class BookingAdminDTO : IMapFrom<Domain.Entities.Booking>
    {
        public int Id { get; set; }
        public BookingStatusDTO BookingStatus { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProjectTopic { get; set; }
        public string CustomerUsername { get; set; }
        public string AuthorUsername { get; set; }
        public int AuthorId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Booking, BookingAdminDTO>()
                .ForMember(d => d.BookingStatus, opt => opt.MapFrom(s => BookingStatusDescriptionValues.GetDescription(s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)))
                .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.HeadProposal.Project.Customer.UserName))
                .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.Id))
                .ForMember(d => d.DueDate, opt => opt.MapFrom(s => BookingStatusGroups.Closed.Contains(s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus) ? s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().DateCreated : s.DateCreated ))
                .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic));
        }
    }
}
