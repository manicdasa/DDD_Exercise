using Braintree;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using System.Threading;
using GhostWriter.Application.Defaults;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;

namespace GhostWriter.Infrastructure.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IApplicationDbContext _context;
        private readonly IBraintreeGateway _braintreeGateway;
        private readonly IBookingPaymentService _bookingPaymentService;
        public static readonly TransactionStatus[] transactionSuccessStatuses = {
                                                                                    TransactionStatus.AUTHORIZED,
                                                                                    TransactionStatus.AUTHORIZING,
                                                                                    TransactionStatus.SETTLED,
                                                                                    TransactionStatus.SETTLING,
                                                                                    TransactionStatus.SETTLEMENT_CONFIRMED,
                                                                                    TransactionStatus.SETTLEMENT_PENDING,
                                                                                    TransactionStatus.SUBMITTED_FOR_SETTLEMENT
                                                                                };

        public BraintreeService(IBraintreeGateway braintreeGateway, IBookingPaymentService bookingPaymentService, IApplicationDbContext context)
        {
            _braintreeGateway = braintreeGateway;
            _bookingPaymentService = bookingPaymentService;
            _context = context;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> MakeRefund(int initiatorUserId, int bookingId, decimal refundAmount, CancellationToken cancellationToken)
        {
            try
            {
                //int customerid;
                //int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out customerid);

                if (!_bookingPaymentService.CustomerRefundCanBeMade(bookingId, refundAmount))
                    return null;

                var transactionId = _bookingPaymentService.GetCustomerBookingPaymentTransactionId(bookingId);

                Result<Transaction> result = _braintreeGateway.Transaction.Refund(transactionId, refundAmount);

                var transactionResult = await _bookingPaymentService.AddBookingTransaction(initiatorUserId, bookingId, refundAmount, Domain.Enums.PaymentType.Refund, string.Empty, result.Message, result.IsSuccess(), result.Transaction, cancellationToken);

                //TODO: SignalR Notification that refund is made

                if (result.IsSuccess())
                {
                    Transaction transaction = result.Target;

                    return new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Success = true,
                        Message = string.Empty
                        //ukljuci transakciju
                    };
                }
                else
                {
                    string errorMessages = "";
                    foreach (ValidationError error in result.Errors.DeepAll())
                    {
                        errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                    }

                    return new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Success = false,
                        Message = errorMessages
                    };
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Message} { ex.InnerException.Message }";

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = false,
                    Message = message
                };
            }

        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> MakePayment(int customerid, int headProposalId, string PaymentMethodNonce, CancellationToken cancellationToken)
        {
            try
            {
                var booking = _context.Bookings.Where(x => x.HeadProposal.Id == headProposalId).FirstOrDefault();

                if (booking is null)
                    return null;

                if (!_bookingPaymentService.CustomerPaymentCanBeMade(booking.Id))
                    return null;

                //TODO: Cena treba da se racuna kao osnovna cena projekta (sto pise u proposalu) + website fee + braintree fee + paypal payout fee
                //Make sure this is correctly done!
                decimal amount = _bookingPaymentService.GetBookingCharges(customerid, booking.Id);

                var request = new TransactionRequest
                {
                    Amount = amount,
                    PaymentMethodNonce = PaymentMethodNonce,
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true
                    }
                };

                Result<Transaction> result = _braintreeGateway.Transaction.Sale(request);

                var transaction = result.Target;

                var transactionDbLog = await _bookingPaymentService.AddBookingTransaction(customerid, booking.Id, amount, Domain.Enums.PaymentType.PaymentToSystem, PaymentMethodNonce,
                                                result.Message, result.IsSuccess(), transaction, cancellationToken);

             
                if (result.IsSuccess())
                {
                    var notificationResult = await _bookingPaymentService.UpdateBookingAfterCustomerPayment(booking.Id, cancellationToken);
                    return notificationResult;
                }
                else
                {
                    return new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Success = false,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
