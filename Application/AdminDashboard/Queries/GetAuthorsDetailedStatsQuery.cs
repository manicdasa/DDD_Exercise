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
using GhostWriter.Application.Common.Helpers;
using System.Collections.Generic;

namespace GhostWriter.Application.AdminDashboard.Queries
{
    public class GetAuthorsDetailedStatsQuery : LookupInputModel, IRequest<PagedList<AuthorDetailedStatsDTO>>
    {
        public string Username { get; set; }
        public UserType UserType { get; set; }
    }

    public class GetAuthorsDetailedStatsQueryHandler : IRequestHandler<GetAuthorsDetailedStatsQuery, PagedList<AuthorDetailedStatsDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetAuthorsDetailedStatsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<AuthorDetailedStatsDTO>> Handle(GetAuthorsDetailedStatsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get dashboard stats.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            try
            {
                var roleId = _userManagementFactory.FindRoleIdByName(UserRoleDefaults.GhostwriterRoleName);
                var users = _context.ApplicationUserRoles.Where(x => x.RoleId == roleId).Select(x => x.ApplicationUser).AsQueryable();

                switch (request.UserType)
                {
                    case UserType.NewUser:
                        users = users.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30));
                        break;
                    case UserType.ActiveUser:
                        var openBidOfferAuthors = _context.Proposals.Where(x => x.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active).Select(x => x.HeadProposal.Ghostwriter);
                        var openBookingAuthors = _context.Bookings.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30) || x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().DateCreated >= DateTime.UtcNow.AddDays(-30)).Select(x => x.HeadProposal.Ghostwriter);
                        users = users.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30)
                                    || openBidOfferAuthors.Contains(x) 
                                    || openBookingAuthors.Contains(x));
                        break;
                    case UserType.InactiveUser:
                        var openBidOfferAuthors1 = _context.Proposals.Where(x => x.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active).Select(x => x.HeadProposal.Ghostwriter);
                        var openBookingAuthors1 = _context.Bookings.Where(x => x.DateCreated >= DateTime.UtcNow.AddDays(-30) || x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().DateCreated >= DateTime.UtcNow.AddDays(-30)).Select(x => x.HeadProposal.Ghostwriter);
                        users = users.Where(x => x.DateCreated <= DateTime.UtcNow.AddDays(-30)
                                    && !openBidOfferAuthors1.Contains(x)
                                    && !openBookingAuthors1.Contains(x));
                        break;
                }

                if (!string.IsNullOrWhiteSpace(request.Search))
                    users = users.Where(x => x.UserName.Contains(request.Search));

                var query = users.Select(x => new AuthorDetailedStatsDTO
                {
                    DateRegistered = x.DateCreated,
                    Id = x.Id,
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                });

                PagedList<AuthorDetailedStatsDTO> retVal;

                if (request.Page != default || request.PageSize != default)
                    retVal = new PagedList<AuthorDetailedStatsDTO>(query, request.Page, request.PageSize);
                else
                    retVal = new PagedList<AuthorDetailedStatsDTO>(query);

                foreach(var stat in retVal.Items)
                {
                    var bookings = _context.Bookings.Where(x => x.HeadProposal.Ghostwriter.UserName == stat.Username).OrderByDescending(x => x.LastUpdate);

                    stat.LastProjectTitle = bookings.FirstOrDefault() == null ? string.Empty : bookings.FirstOrDefault().HeadProposal.Project.ProjectTopic;
                    stat.LastProjectId = bookings.FirstOrDefault() == null ? default(int) : bookings.FirstOrDefault().HeadProposal.Project.Id;
                    stat.NoClosedProjects = _context.Bookings.Where(x => x.HeadProposal.Project.Customer.UserName == stat.Username && BookingStatusGroups.Closed.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count();
                    stat.NoActiveProjects = bookings.Count() - stat.NoClosedProjects;
                    stat.NoTotalProjects = bookings.Count();
                }

                List<AuthorDetailedStatsDTO> queryOrdered = retVal.Items;
                if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
                {
                    queryOrdered = LinqHelper.OrderByPropertyName<AuthorDetailedStatsDTO>(queryOrdered.AsQueryable(), request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc).ToList();
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
