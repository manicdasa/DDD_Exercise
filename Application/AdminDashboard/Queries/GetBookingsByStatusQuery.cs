using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
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
using System.Linq.Expressions;
using GhostWriter.Application.Common.Mappings;
using System.Reflection;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.AdminDashboard.Queries
{

    public class GetBookingsByStatusQuery : LookupInputModel, IRequest<PagedList<BookingAdminDTO>>
    {
       public List<BookingStatus> BookingStatuses { get; set; }
        public string Username { get; set; }
    }

    public class GetBookingsQueryExtendedHandler : IRequestHandler<GetBookingsByStatusQuery, PagedList<BookingAdminDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetBookingsQueryExtendedHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<BookingAdminDTO>> Handle(GetBookingsByStatusQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get project information.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            var query = _context.Bookings.Where(x => request.BookingStatuses.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus))
                                .OrderByDescending(x => x.LastUpdate)
                                .ProjectTo<BookingAdminDTO>(_mapper.ConfigurationProvider);
              
            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(x => x.ProjectTopic.ToLower().Contains(request.Search.ToLower())
                            || x.CustomerUsername.ToLower().Contains(request.Search.ToLower())
                            || x.AuthorUsername.ToLower().Contains(request.Search.ToLower()));

            IEnumerable<BookingAdminDTO> queryOrdered = query;

            if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
            {
                queryOrdered = LinqHelper.OrderByPropertyName<BookingAdminDTO>(query, request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc);
            }

            if (request.Page != default || request.PageSize != default)
                return new PagedList<BookingAdminDTO>(queryOrdered, request.Page, request.PageSize);
            else
                return new PagedList<BookingAdminDTO>(queryOrdered);
        }
    }
}
