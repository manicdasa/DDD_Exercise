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
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.AdminDashboard.Queries
{
    public class GetDashboardStatsQuery : IRequest<DashboardStatsDTO>
    {
        public string Username { get; set; }
    }

    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetDashboardStatsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<DashboardStatsDTO> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get dashboard stats.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            try
            {
                var roleId = _userManagementFactory.FindRoleIdByName(UserRoleDefaults.CustomerRoleName);
                var users = _context.ApplicationUserRoles.Where(x => x.RoleId == roleId).Select(x => x.ApplicationUser).AsQueryable();


                DashboardStatsDTO retVal = new DashboardStatsDTO()
                {
                    ActiveProjects = _context.Bookings.Where(x => BookingStatusGroups.ActiveNoDispute.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count(),
                    DisputeProjects = _context.Bookings.Where(x => BookingStatusGroups.InDispute.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count(),
                    NewProjects = _context.Bookings.Where(x => BookingStatusGroups.Inactive.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count(),
                    NewUsers =  users.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30)).Count()
            };



                //DashboardStatsDTO retVal = new DashboardStatsDTO()
                //{
                //    ActiveProjects = _context.Bookings.Where(x => BookingStatusGroups.ActiveNoDispute.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count(),
                //    DisputeProjects = _context.Bookings.Where(x => BookingStatusGroups.InDispute.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count(),
                //    NewProjects = _context.Projects.Where(x => x.DateCreated > DateTime.UtcNow.AddDays(-1)).Count(),
                //    NewUsers = _context.ApplicationUsers.Where(x => x.DateCreated > DateTime.UtcNow.AddDays(-1)).Count()
                //};

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
