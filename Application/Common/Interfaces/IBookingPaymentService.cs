using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IBookingPaymentService
    {
        /// <summary>
        /// Gets total charging amount of the booking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookingId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        decimal GetBookingCharges(int userId, int bookingId);

        /// <summary>
        /// Adds transaction information of the booking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookingId"></param>
        /// <param name="paymentAMount"></param>
        /// <param name="paymentMethodNonce"></param>
        /// <param name="isSuccess"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OutputModel> AddBookingTransaction(int userId, int bookingId, decimal paymentAmount, PaymentType paymentType, string paymentMethodNonce, string billingAddress, bool isSuccess, Braintree.Transaction transaction, CancellationToken cancellationToken);

        /// <summary>
        /// Updates booking status history after customer's payment
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExtendedOutputModelList<NotificationSignalRDTO>> UpdateBookingAfterCustomerPayment(int bookingId, CancellationToken cancellationToken);

        /// <summary>
        /// Detects weather the refund can be make. Checks if the customer has already paid, the project is confirmed by customer, and the total amount paid is smaller than the refund amount
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="refundAmount"></param>
        /// <returns></returns>
        bool CustomerRefundCanBeMade(int bookingId, decimal refundAmount);

        /// <summary>
        /// Detects weather customer can pay for the project. Checks if the booking status is inactive
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        bool CustomerPaymentCanBeMade(int bookingId);

        /// <summary>
        /// Get the PaymentMethodNonce of the customer's payment transaction
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        string GetCustomerBookingPaymentTransactionId(int bookingId);
    }
}
