using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface  IBraintreeService
    {
        Task<ExtendedOutputModelList<NotificationSignalRDTO>> MakeRefund(int initiatorUserId, int bookingId, decimal refundAmount, CancellationToken cancellationToken);
        Task<ExtendedOutputModelList<NotificationSignalRDTO>> MakePayment(int customerid, int headProposalId, string PaymentMethodNonce, CancellationToken cancellationToken);
    }
}
