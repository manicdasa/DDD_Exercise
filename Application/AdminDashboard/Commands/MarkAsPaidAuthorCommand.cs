using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Enums;
using System;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Application.AdminDashboard.Commands
{
    public class MarkAsPaidAuthorCommand : IRequest<OutputModel>
    {
        public int BookingId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string AdminUsername { get; set; }

        public class PayAuthorCommandHandler : IRequestHandler<MarkAsPaidAuthorCommand, OutputModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IUserManagementFactory _userManagementFactory;

            public PayAuthorCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory)
            {
                _context = context;
                _userManagementFactory = userManagementFactory;
            }

            public async Task<OutputModel> Handle(MarkAsPaidAuthorCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.AdminUsername))
                    throw new AuthorizationException($"User is unauthorized to confirm a project.");

                var user = await _userManagementFactory.FindUser(request.AdminUsername);

                if (user is null)
                    throw new NotFoundException($"User {request.AdminUsername} not found.");

                var booking = _context.Bookings.Find(request.BookingId);

                if (booking is null)
                    throw new NotFoundException($"Booking {request.BookingId} not found.");

                var ghwData = _userManagementFactory.GetUsersAdditionalData(booking.HeadProposal.Ghostwriter.UserName, UserRoleDefaults.GhostwriterRoleName).FirstOrDefault();

                if(ghwData == null || string.IsNullOrWhiteSpace(ghwData.IBAN))
                    throw new Exception($"No author payment details found.");

                try
                {
                    Transaction transaction = new Transaction()
                    {
                        Booking = booking,
                        DateCreated = DateTime.UtcNow,
                        IsSuccessful = true,
                        Note = "Manual IBAN payment to author",
                        PaymentType = PaymentType.PaymentToGhostWriter,
                        TotalAmount = request.PaymentAmount
                    };

                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync(cancellationToken);

                    return new OutputModel()
                    {
                        Success = true,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.InnerException.Message}");
                }
            }
        }
    }
}
