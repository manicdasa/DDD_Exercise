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
using GhostWriter.Domain.Enums;
using GhostWriter.Domain.Defaults;

namespace GhostWriter.Application.AdminDashboard.Queries
{
    public class GetCustomersStatsQuery : LookupInputModel, IRequest<PagedList<CustomerStatsDTO>>
    {
        public string Username { get; set; }
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersStatsQuery, PagedList<CustomerStatsDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<CustomerStatsDTO>> Handle(GetCustomersStatsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get dashboard stats.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            try
            {
                var query = _context.Bookings
                   .GroupBy(x => x.HeadProposal.Project.Customer.UserName)
                   .Select(x => new CustomerStatsDTO
                   {
                       Username = x.Key,
                       NoActiveProjects = x.Count()
                   });

                if (!string.IsNullOrWhiteSpace(request.Search))
                    query = query.Where(x => x.Username.Contains(request.Search));

                PagedList<CustomerStatsDTO> retVal;

                if (request.Page != default || request.PageSize != default)
                    retVal = new PagedList<CustomerStatsDTO>(query, request.Page, request.PageSize);
                else
                    retVal = new PagedList<CustomerStatsDTO>(query);

                foreach(var stat in retVal.Items)
                {
                    var cust = await _userManagementFactory.FindUser(stat.Username);
                    stat.DateRegistered = cust.DateCreated;
                    stat.NoClosedProjects = _context.Bookings.Where(x => x.HeadProposal.Project.Customer.UserName == stat.Username && BookingStatusGroups.Closed.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count();
                    stat.NoActiveProjects -= stat.NoClosedProjects;
                }

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }
    }
}
