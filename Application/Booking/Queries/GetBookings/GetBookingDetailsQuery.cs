using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Booking.Commands.GetBookings
{
    public class GetBookingDetailsQuery : IRequest<BookingDetailsDTO>
    {
        public string Username { get; set; }

        public int BookingId { get; set; }
    }

    public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsQuery, BookingDetailsDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetBookingDetailsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<BookingDetailsDTO> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to get closed projects.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            var booking = _context.Bookings.Find(request.BookingId);

            if (booking is null)
                throw new NotFoundException($"Booking {request.BookingId} not found.");

            if (!_userManagementFactory.IsInRole(user, UserRoleDefaults.AdminRoleName) && booking.HeadProposal.Ghostwriter.UserName != request.Username && booking.HeadProposal.Project.Customer.UserName != request.Username)
                throw new AuthorizationException($"User is unauthorized to access the booking details.");

            var retVal = _context.Bookings.Where(x => x.Id == request.BookingId)
                                        .ProjectTo<BookingDetailsDTO>(_mapper.ConfigurationProvider)
                                        .FirstOrDefault();

            var rate = _context.Rates.Where(x => x.Booking.Id == booking.Id).FirstOrDefault();
            if (rate != null)
                retVal.RatingDTO = new RatingDTO() { Id = rate.Id, StarRating = rate.StarRating };

            return retVal;
        }
    }
}
