using AutoMapper;
using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models.Chat;
using System.Threading.Tasks;

namespace GhostWriter.Infrastructure.Services
{
    public class ConversationService: IConversationService
    {

        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public ConversationService(IApplicationDbContext context, IMapper mapper, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<int> GetConversationIdFromHeadProposalId(ChatConversationQuery request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to access the booking chat.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            var headProposal = _context.HeadProposals.Find(request.HeadProposalId);

            if (headProposal == null)
                throw new NotFoundException($"Head proposal {request.HeadProposalId} not found.");

            return headProposal.Conversation.Id;

        }

        public async Task<int> GetConversationIdFromBookingId(ChatConversation1Query request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to access the booking chat.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            var booking = _context.Bookings.Find(request.BookingId);

            if (booking == null)
                throw new NotFoundException($"Booking {request.BookingId} not found.");

            if (booking.HeadProposal.Ghostwriter.UserName != request.Username && booking.HeadProposal.Project.Customer.UserName != request.Username)
                throw new AuthorizationException($"User is unauthorized to access the booking chat.");

            var headProposal = _context.HeadProposals.Find(booking.HeadProposal.Id);

            if (headProposal == null)
                throw new NotFoundException($"Head proposal {request.BookingId} not found.");

            return headProposal.Conversation.Id;

        }
    }
}
