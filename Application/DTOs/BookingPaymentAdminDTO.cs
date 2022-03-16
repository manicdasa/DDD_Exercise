using System;
using System.Linq;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class BookingPaymentAdminDTO : BookingAdminDTO, IMapFrom<Domain.Entities.Booking>
    {
        public decimal TotalAmountToPay { get; set; }
        public decimal CustomerRefund { get; set; }
        public decimal AmountPaid { get; set; }
        public string IBAN { get; set; }
        public bool IsCompletelyPaid { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Booking, BookingPaymentAdminDTO>()
                .ForMember(d => d.BookingStatus, opt => opt.MapFrom(s => BookingStatusDescriptionValues.GetDescription(s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)))
                .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.HeadProposal.Project.Customer.UserName))
                .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.Id))
                .ForMember(d => d.TotalAmountToPay, opt => opt.MapFrom(s => (s.Disputes.OrderByDescending(y => y.DateCreated).FirstOrDefault().DisputeStatus == DisputeStatus.Accepted ? s.Disputes.OrderByDescending(y => y.DateCreated).FirstOrDefault().PaymentToAuthor : s.TotalPrice)))
                .ForMember(d => d.DueDate, opt => opt.MapFrom(s => BookingStatusGroups.Closed.Contains(s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus) ? s.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().DateCreated : s.DateCreated))
                .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic));
        }
    }
}
