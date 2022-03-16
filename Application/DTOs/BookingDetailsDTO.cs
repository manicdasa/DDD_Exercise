using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Defaults;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class BookingDetailsDTO : IMapFrom<Domain.Entities.Booking>
    {
        public int BookingId { get; set; }
        public int HeadProposalId { get; set; }
        public DateTime Deadline { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TaxPercentage { get { return BusinessDefaults.TaxPercentage; } }
        public decimal FeePerPage { get { return BusinessDefaults.FeePerPage; } }
        public bool PaymentVerified { get { return true; }  }
        public string ProjectTopic { get; set; }
        public string ProjectDescription { get; set; }
        public int PagesNo { get; set; }
        public BookingStatusDTO BookingStatus { get; set; }
        public CustomerPublicInfoDTO CustomerDTO { get; set; }
        public string AuthorUsername { get; set; }
        public int AuthorId { get; set; }
        public string DisputeMessage { get; set; }
        public RatingDTO RatingDTO { get; set; }
        public LanguageDTO LanguageDTO { get; set; }
        public KindOfWorkDTO KindOfWorkDTO { get; set; }
        public List<ExpertiseAreaDTO> ExpertiseAreaDTOs { get; set; }
        public List<DocumentDTO> DocumentDTOs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Booking, BookingDetailsDTO>()
                .ForMember(d => d.BookingId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.HeadProposalId, opt => opt.MapFrom(s => s.HeadProposal.Id))
                .ForMember(d => d.CustomerDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.Customer.UserRoles.Where(x => x.UserId == s.HeadProposal.Project.CustomerId).FirstOrDefault().UserRoleData))
                .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.Id))
                .ForMember(d => d.Deadline, opt => opt.MapFrom(s => s.HeadProposal.Project.Deadline))
                .ForMember(d => d.BookingStatus, opt => opt.MapFrom(s => BookingStatusDescriptionValues.GetDescription(s.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus)))
                .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic))
                .ForMember(d => d.ProjectDescription, opt => opt.MapFrom(s => s.HeadProposal.Project.Description))
                .ForMember(d => d.PagesNo, opt => opt.MapFrom(s => s.HeadProposal.Project.PagesNo))
                .ForMember(d => d.DisputeMessage, opt => opt.MapFrom(s => s.Disputes.OrderByDescending(x => x.DateCreated).FirstOrDefault() != null ? s.Disputes.OrderByDescending(x => x.DateCreated).FirstOrDefault().Reason : string.Empty))
                .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.Language))
                .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.KindOfWork))
                .ForMember(d => d.ExpertiseAreaDTOs, opt => opt.MapFrom(s => s.HeadProposal.Project.ExpertiseAreas))
                .ForMember(d => d.DocumentDTOs, opt => opt.MapFrom(s => s.Documents));
        }
    }
}
