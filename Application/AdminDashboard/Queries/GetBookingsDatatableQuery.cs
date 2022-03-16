using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Common.Helpers;
using System.Collections.Generic;

namespace GhostWriter.Application.AdminDashboard.Queries
{
    public class GetBookingsDatatableQuery : LookupInputModel, IRequest<PagedList<BookingShortInfoDTO>>
    {
        public string Username { get; set; }
    }

    public class GetBookingsDatatableQueryHandler : IRequestHandler<GetBookingsDatatableQuery, PagedList<BookingShortInfoDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetBookingsDatatableQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<BookingShortInfoDTO>> Handle(GetBookingsDatatableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Bookings.OrderByDescending(x => x.LastUpdate)
                            .ProjectTo<BookingShortInfoDTO>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrWhiteSpace(request.Search))
                    query = query.Where(x => x.ProjectTopic.ToLower().Contains(request.Search.ToLower())
                                || x.CustomerUsername.ToLower().Contains(request.Search.ToLower())
                                || x.AuthorUsername.ToLower().Contains(request.Search.ToLower()));

                IEnumerable< BookingShortInfoDTO> queryOrdered = query;

                if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
                {
                    queryOrdered = LinqHelper.OrderByPropertyName<BookingShortInfoDTO>(query, request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc);
                }
                if (request.Page != default || request.PageSize != default)
                    return new PagedList<BookingShortInfoDTO>(queryOrdered, request.Page, request.PageSize);
                else
                    return new PagedList<BookingShortInfoDTO>(queryOrdered);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
