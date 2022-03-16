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
    public class GetAuthorsStatsQuery : LookupInputModel, IRequest<PagedList<AuthorStatsDTO>>
    {
        public string Username { get; set; }
    }

    public class GetAuthorsStatsQueryHandler : IRequestHandler<GetAuthorsStatsQuery, PagedList<AuthorStatsDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetAuthorsStatsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<AuthorStatsDTO>> Handle(GetAuthorsStatsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get dashboard stats.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            try
            {
                var query = _context.Bookings
                   .GroupBy(x => x.HeadProposal.Ghostwriter.UserName)
                   .Select(x => new AuthorStatsDTO
                   {
                       Username = x.Key,
                       NoActiveProjects = x.Count()
                   });

                if (!string.IsNullOrWhiteSpace(request.Search))
                    query = query.Where(x => x.Username.Contains(request.Search));

                PagedList<AuthorStatsDTO> retVal;

                if (request.Page != default || request.PageSize != default)
                    retVal = new PagedList<AuthorStatsDTO>(query, request.Page, request.PageSize);
                else
                    retVal = new PagedList<AuthorStatsDTO>(query);

                foreach(var stat in retVal.Items)
                {
                    var author = await _userManagementFactory.FindUser(stat.Username);
                    stat.DateRegistered = author.DateCreated;
                    stat.FirstName = author.FirstName;
                    stat.LastName = author.LastName;
                    stat.NoClosedProjects = _context.Bookings.Where(x => x.HeadProposal.Project.Customer.UserName == stat.Username 
                                        && BookingStatusGroups.Closed.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Count();
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
