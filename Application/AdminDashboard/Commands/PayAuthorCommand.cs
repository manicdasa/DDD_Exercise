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
    public class PayAuthorCommand : IRequest<OutputModel>
    {
        public int BookingId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string AdminUsername { get; set; }

        public class PayAuthorCommandHandler : IRequestHandler<PayAuthorCommand, OutputModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IUserManagementFactory _userManagementFactory;
            private readonly IPayoutService _payoutService;

            public PayAuthorCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IPayoutService payoutService)
            {
                _context = context;
                _userManagementFactory = userManagementFactory;
                _payoutService = payoutService;
            }

            public async Task<OutputModel> Handle(PayAuthorCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.AdminUsername))
                    throw new AuthorizationException($"User is unauthorized to confirm a project.");

                var user = await _userManagementFactory.FindUser(request.AdminUsername);

                if (user is null)
                    throw new NotFoundException($"User {request.AdminUsername} not found.");

                var booking = _context.Bookings.Find(request.BookingId);

                if (booking is null)
                    throw new NotFoundException($"Booking {request.BookingId} not found.");

                //TODO: we will validate payment later
                //var finalVersionStatus = booking.BookingStatusHistories.Where(x => BookingStatusGroups.FinalVersionSubmitted.Contains(x.BookingStatus)).OrderByDescending(x => x.DateCreated).FirstOrDefault();
                //if (finalVersionStatus == null || finalVersionStatus.Document == null)
                //    throw new Exception($"Payment cannot be made for this booking because the final project's version hasn't been submitted yet.");

                //var lastDispute = booking.Disputes.OrderByDescending(x => x.DateCreated).FirstOrDefault();
                //if (lastDispute != null && lastDispute.DisputeStatus == DisputeStatus.Active)
                //    throw new Exception($"Payment cannot be made for this booking because it has an active nonresolved dispute.");

                var ghwData = _userManagementFactory.GetUsersAdditionalData(booking.HeadProposal.Ghostwriter.UserName, UserRoleDefaults.GhostwriterRoleName).FirstOrDefault();

                if(ghwData == null || string.IsNullOrWhiteSpace(ghwData.PaypalPayerID))
                    throw new Exception($"No author payment details found.");

                try
                {
                    var payoutResult =  await _payoutService.CreatePayoutInEuros(ghwData.PaypalPayerID, request.PaymentAmount);

                    Transaction transaction = new Transaction()
                    {
                        Booking = booking,
                        DateCreated = DateTime.UtcNow,
                        IsSuccessful = payoutResult.Success,
                        Note = payoutResult.Message,
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
                    throw new Exception($"{ex.Message}");
                }
            }
        }
    }
}
