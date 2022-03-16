﻿using System;
using System.Linq;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class BookingShortInfoDTO : IMapFrom<Domain.Entities.Booking>
    {
        public int BookingId { get; set; }
        public int HeadProposalId { get; set; }
        public BookingStatusDTO BookingStatus { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalServiceCharges { get; set; }
        public string ProjectTopic { get; set; }
        public int PagesNo { get; set; }
        public string CustomerUsername { get; set; }
        public string AuthorUsername { get; set; }
        public int AuthorId { get; set; }
        public LanguageDTO LanguageDTO { get; set; }
        public KindOfWorkDTO KindOfWorkDTO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Booking, BookingShortInfoDTO>()
                .ForMember(d => d.BookingId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.HeadProposalId, opt => opt.MapFrom(s => s.HeadProposal.Id))
                .ForMember(d => d.BookingStatus, opt => opt.MapFrom(s => BookingStatusDescriptionValues.GetDescription(s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)))
                .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.HeadProposal.Project.Customer.UserName))
                .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.Id))
                .ForMember(d => d.Deadline, opt => opt.MapFrom(s => s.HeadProposal.Project.Deadline))
                .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic))
                .ForMember(d => d.PagesNo, opt => opt.MapFrom(s => s.HeadProposal.Project.PagesNo))
                .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.Language))
                .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.KindOfWork));
        }
    }
}
