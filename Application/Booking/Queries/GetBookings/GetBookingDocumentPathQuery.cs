using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Application.Booking.Commands.GetBookings
{
    public class GetBookingDocumentQuery : IRequest<Document>
    {
        public int DocumentId { get; set; }
        public string Username { get; set; }
    }

    public class GetBookingDocumentQueryHandler : IRequestHandler<GetBookingDocumentQuery, Document>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetBookingDocumentQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<Document> Handle(GetBookingDocumentQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to access the document.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            var document = _context.Documents.Find(request.DocumentId);

            if (document == null)
                throw new NotFoundException($"Document {request.DocumentId} not found.");

            if (document.Booking.HeadProposal.Ghostwriter.UserName != request.Username && document.Booking.HeadProposal.Project.Customer.UserName != request.Username)
                throw new AuthorizationException($"User is unauthorized to access the document.");

            return document;
        }
    }
}
