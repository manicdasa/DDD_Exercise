using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Defaults;

namespace GhostWriter.Application.Booking.Commands.GetBookings
{
    public class GetBookingChatInfoQuery : IRequest<PagedList<BookingChatInfoDTO>>
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public class GetBookingChatInfoQueryHandler : IRequestHandler<GetBookingChatInfoQuery, PagedList<BookingChatInfoDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetBookingChatInfoQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<BookingChatInfoDTO>> Handle(GetBookingChatInfoQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get {request.RoleName}'s closed projects.");

            if (!(request.RoleName == UserRoleDefaults.CustomerRoleName || request.RoleName == UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User is unauthorized to get {request.RoleName}'s closed projects.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            if ((request.RoleName == UserRoleDefaults.GhostwriterRoleName && !_userManagementFactory.IsInRole(user, UserRoleDefaults.GhostwriterRoleName)
                || (request.RoleName == UserRoleDefaults.CustomerRoleName && !_userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName))))
                throw new AuthorizationException($"User {request.Username} is unauthorized to access {request.RoleName}'s closed projects tab.");

            var query = _context.Bookings.Where(x => 
                    (x.HeadProposal.GHWId == user.Id || x.HeadProposal.Project.CustomerId == user.Id)
                    && BookingStatusGroups.Open.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus))
                .OrderByDescending(x => x.LastUpdate)
                .ProjectTo<BookingChatInfoDTO>(_mapper.ConfigurationProvider);

                return new PagedList<BookingChatInfoDTO>(query);
        }
    }
}
