using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Helpers;
using System;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Enums;
using GhostWriter.Domain.Defaults;
using System.Collections.Generic;

namespace GhostWriter.Application.AdminDashboard.Queries
{
    public enum UserType
    {
        NewUser,
        ActiveUser,
        InactiveUser
    }
    public class GetCustomersDetailedStatsQuery : LookupInputModel, IRequest<PagedList<CustomerDetailedStatsDTO>>
    {
        public string Username { get; set; }
        public UserType UserType { get; set; }
    }

    public class GetCustomersDetailedStatsQueryHandler : IRequestHandler<GetCustomersDetailedStatsQuery, PagedList<CustomerDetailedStatsDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetCustomersDetailedStatsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<CustomerDetailedStatsDTO>> Handle(GetCustomersDetailedStatsQuery request, CancellationToken cancellationToken)
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

                switch (request.UserType)
                {
                    case UserType.NewUser:
                        users = users.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30));
                        break;
                    case UserType.ActiveUser:
                        var openProjectCustomers = _context.Projects.Where(x => x.ProjectStatus == ProjectStatus.InCreation || x.ProjectStatus == ProjectStatus.Open).Select(x => x.Customer);
                        var openBookingCustomers = _context.Bookings.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30) || x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().DateCreated >= DateTime.UtcNow.AddDays(-30)).Select(x => x.HeadProposal.Project.Customer);
                        users = users.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30)
                                    || openProjectCustomers.Contains(x) 
                                    || openBookingCustomers.Contains(x));
                        break;
                    case UserType.InactiveUser:
                        var openProjectCustomers1 = _context.Projects.Where(x => x.ProjectStatus == ProjectStatus.InCreation || x.ProjectStatus == ProjectStatus.Open).Select(x => x.Customer);
                        var openBookingCustomers1 = _context.Bookings.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30) || x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().DateCreated >= DateTime.UtcNow.AddDays(-30)).Select(x => x.HeadProposal.Project.Customer);
                        users = users.Where(x => x.DateCreated <= DateTime.UtcNow.AddDays(-30)
                                    && !openProjectCustomers1.Contains(x)
                                    && !openBookingCustomers1.Contains(x));
                        break;
                }

                if (!string.IsNullOrWhiteSpace(request.Search))
                    users = users.Where(x => x.UserName.Contains(request.Search));

                var query = users.Select(x => new CustomerDetailedStatsDTO
                {
                    Id = x.Id,
                    DateRegistered = x.DateCreated,
                    Username = x.UserName
                });

                PagedList<CustomerDetailedStatsDTO> retVal;

                if (request.Page != default || request.PageSize != default)
                    retVal = new PagedList<CustomerDetailedStatsDTO>(query, request.Page, request.PageSize);
                else
                    retVal = new PagedList<CustomerDetailedStatsDTO>(query);

                foreach(var stat in retVal.Items)
                {
                    var projects = _context.Projects.Where(x => x.Customer.UserName == stat.Username).OrderByDescending(x => x.DateCreated);
                    var bookings = _context.Bookings.Where(x => x.HeadProposal.Project.Customer.UserName == stat.Username);

                    stat.LastProjectTitle = projects.FirstOrDefault() == null ? string.Empty : projects.FirstOrDefault().ProjectTopic;
                    stat.LastProjectId = projects.FirstOrDefault() == null ? default(int) : projects.FirstOrDefault().Id;
                    stat.NoClosedProjects = _context.Bookings.Where(x => x.HeadProposal.Project.Customer.UserName == stat.Username && BookingStatusGroups.Closed.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count();
                    stat.NoActiveProjects = _context.Bookings.Where(x => x.HeadProposal.Project.Customer.UserName == stat.Username).Count() - stat.NoClosedProjects;
                    stat.NoProjectsCreated = projects.Count();
                    stat.NoUnassignedProjects = stat.NoProjectsCreated - (stat.NoActiveProjects + stat.NoClosedProjects);
                }

               

                List<CustomerDetailedStatsDTO> queryOrdered = retVal.Items;

                if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
                {
                    queryOrdered = LinqHelper.OrderByPropertyName<CustomerDetailedStatsDTO>(queryOrdered.AsQueryable(), request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc).ToList();
                }

                retVal.Items = queryOrdered;


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
