using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models.Shared;
using AutoMapper.QueryableExtensions;
using GhostWriter.Domain.Enums;
using System;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Domain.Defaults;

namespace GhostWriter.Application.AdminDashboard.Queries
{
    public class GetClosedUnpaidProjectsQuery : LookupInputModel, IRequest<PagedList<BookingPaymentAdminDTO>>
    {
       public List<BookingStatus> BookingStatuses { get; set; }
        public string Username { get; set; }
    }

    public class GetClosedUnpaidProjectsQueryHandler : IRequestHandler<GetClosedUnpaidProjectsQuery, PagedList<BookingPaymentAdminDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetClosedUnpaidProjectsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<BookingPaymentAdminDTO>> Handle(GetClosedUnpaidProjectsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get project information.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            var query = _context.Bookings.Where(x => request.BookingStatuses.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus))
                                .OrderByDescending(x => x.LastUpdate)
                                .ProjectTo<BookingPaymentAdminDTO>(_mapper.ConfigurationProvider);

            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(x => x.ProjectTopic.ToLower().Contains(request.Search.ToLower())
                            || x.CustomerUsername.ToLower().Contains(request.Search.ToLower())
                            || x.AuthorUsername.ToLower().Contains(request.Search.ToLower()));



            PagedList<BookingPaymentAdminDTO> retVal;

            if (request.Page != default || request.PageSize != default)
                retVal = new PagedList<BookingPaymentAdminDTO>(query, request.Page, request.PageSize);
            else
                retVal = new PagedList<BookingPaymentAdminDTO>(query);

            foreach(var bookingDTO in retVal.Items)
            {
                var userAdditionalData =_userManagementFactory.GetUsersAdditionalData(bookingDTO.AuthorUsername, UserRoleDefaults.GhostwriterRoleName).FirstOrDefault();
                if (userAdditionalData != null)  bookingDTO.IBAN = userAdditionalData.IBAN;
                
                bookingDTO.AmountPaid = _context.Transactions.Where(x => x.PaymentType == PaymentType.PaymentToGhostWriter && x.IsSuccessful && x.Booking.Id == bookingDTO.Id).Sum(x => x.TotalAmount);
                bookingDTO.CustomerRefund = _context.Transactions.Where(x => x.IsSuccessful && x.PaymentType == PaymentType.Refund && x.Booking.Id == bookingDTO.Id).Sum(x => x.TotalAmount);
                bookingDTO.IsCompletelyPaid = bookingDTO.AmountPaid == bookingDTO.TotalAmountToPay;
            }

            List<BookingPaymentAdminDTO> queryOrdered = retVal.Items;
            if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
            {
               queryOrdered = LinqHelper.OrderByPropertyName<BookingPaymentAdminDTO>(queryOrdered.AsQueryable(), request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc).ToList();
            }
            retVal.Items = queryOrdered;

            return retVal;
        }
    }
}
