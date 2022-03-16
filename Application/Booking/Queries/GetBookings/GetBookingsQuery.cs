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
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Application.Booking.Commands.GetBookings
{
    public class GetAuthorBookingsQuery : PaginationModel
    {
        public List<int> KindOfWorkIds { get; set; }
        public List<int> AreaOfExpertiseIds { get; set; }
        public List<int> LanguageIds { get; set; }
        public int? MinimumDegreeId { get; set; }
        public int? NoPagesFromRange { get; set; }
        public int? NoPagesToRange { get; set; }
    }

    public class GetCustomerBookingsQuery : GetAuthorBookingsQuery, IMapFrom<GetAuthorBookingsQuery>
    {
        public DateTime Deadline { get; set; }
    }
    public class GetBookingsQueryExtended : GetCustomerBookingsQuery, IRequest<PagedList<BookingShortInfoDTO>>
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
        public List<BookingStatus> BookingStatuses { get; set; }
    }

    public class GetBookingsQueryExtendedHandler : IRequestHandler<GetBookingsQueryExtended, PagedList<BookingShortInfoDTO>>
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

        public async Task<PagedList<BookingShortInfoDTO>> Handle(GetBookingsQueryExtended request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get {request.RoleName}'s closed projects.");

            if (!(request.RoleName == UserRoleDefaults.CustomerRoleName || request.RoleName == UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User is unauthorized to get {request.RoleName}'s closed projects.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            if ((request.RoleName == UserRoleDefaults.GhostwriterRoleName && !_userManagementFactory.IsInRole(user, UserRoleDefaults.GhostwriterRoleName) 
                || (request.RoleName == UserRoleDefaults.CustomerRoleName && !_userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName))))
                throw new AuthorizationException($"User {request.Username} is unauthorized to access {request.RoleName}'s closed projects tab.");

            Degree minimumDegree = request.MinimumDegreeId is null ? null : _context.Degrees.Find(request.MinimumDegreeId);

            var query = _context.Bookings.Where(x => (request.RoleName == UserRoleDefaults.GhostwriterRoleName ? x.HeadProposal.GHWId == user.Id : x.HeadProposal.Project.CustomerId == user.Id)
                                                && request.BookingStatuses.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)
                                                 && (!request.LanguageIds.Any() || request.LanguageIds.Contains(x.HeadProposal.Project.Language.Id))
                                                && (!request.KindOfWorkIds.Any() || request.KindOfWorkIds.Contains(x.HeadProposal.Project.KindOfWorkId))
                                                 && (request.MinimumDegreeId == null || x.HeadProposal.Project.MinimumDegree.Stage <= minimumDegree.Stage)
                                                && (request.NoPagesFromRange == null || (request.NoPagesFromRange <= x.HeadProposal.Project.PagesNo))
                                                && (request.NoPagesToRange == null || (request.NoPagesToRange >= x.HeadProposal.Project.PagesNo))
                                                && (request.Deadline == default(DateTime) || (DateTime)request.Deadline >= x.HeadProposal.Project.Deadline));

            if (request.AreaOfExpertiseIds != null && request.AreaOfExpertiseIds.Any())
            {
                List<int> projectIds = new List<int>();
                foreach (var exparId in request.AreaOfExpertiseIds ?? new List<int>())
                {
                    var expertiseArea = _context.ExpertiseAreas.Find(exparId);

                    if (expertiseArea != null)
                    {
                        projectIds.AddRange(query.Where(x => x.HeadProposal.Project.ExpertiseAreas.Contains(expertiseArea)).Select(x => x.Id));
                    }
                }
                query = query.Where(x => projectIds.Contains(x.Id));
            }

            var projects = query
                .OrderByDescending(x => x.LastUpdate)
                .ProjectTo<BookingShortInfoDTO>(_mapper.ConfigurationProvider);

            PagedList<BookingShortInfoDTO> retVal;

            if (request.Page != default || request.PageSize != default)
                retVal = new PagedList<BookingShortInfoDTO>(projects, request.Page, request.PageSize);
            else
                retVal = new PagedList<BookingShortInfoDTO>(projects);

            return retVal;
        }
    }
}
