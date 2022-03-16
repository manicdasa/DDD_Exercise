using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Services
{
    class BookingPaymentService : IBookingPaymentService
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public BookingPaymentService(IApplicationDbContext context, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
        }

        public async Task<OutputModel> AddBookingTransaction(int userId, int bookingId, decimal paymentAmount, PaymentType paymentType, string paymentMethodNonce, string transactionMessage, bool isSuccess, Braintree.Transaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var booking = _context.Bookings.Find(bookingId);

                if (booking == null)
                    throw new NotFoundException($"Booking {bookingId} not found.");

                var bookingTransaction = new Domain.Entities.Transaction()
                {
                    Booking = booking,
                    DateCreated = DateTime.UtcNow,
                    TotalAmount = paymentAmount,
                    PaymentMethodNonce = paymentMethodNonce,
                    PaymentType = paymentType,
                    TransactionMessage = transactionMessage,
                    TransactionId = transaction != null ? transaction.Id : string.Empty,
                    IsSuccessful = isSuccess
                };
                _context.Transactions.Add(bookingTransaction);

                if (isSuccess)
                {
                    var message = new Message()
                    {
                        Conversation = booking.HeadProposal.Conversation,
                        DateTimeSent = DateTime.UtcNow,
                        IsLogMessage = true,
                        MessageText = $"{paymentType.ToString()} is made."
                    };

                    var notification1 = new Notification()
                    {
                        DateTimeCreated = DateTime.UtcNow,
                        Message = $"Refund for your project '{booking.HeadProposal.Project.ProjectTopic}' is made.",
                        DetailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id),
                        IsSeen = false,
                        NotificationType = NotificationType.ActiveProject,
                        Receiver = booking.HeadProposal.Project.Customer
                    };

                    var notification2 = new Notification()
                    {
                        DateTimeCreated = DateTime.UtcNow,
                        Message = $"Refund for project '{booking.HeadProposal.Project.ProjectTopic}' is made.",
                        DetailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id),
                        IsSeen = false,
                        NotificationType = NotificationType.ActiveProject,
                        Receiver = _userManagementFactory.FindUserById(userId)
                    };
                    _context.Notifications.Add(notification1);
                    _context.Notifications.Add(notification2);
                    _context.Messages.Add(message);
                }
              
                await _context.SaveChangesAsync(cancellationToken);

                return new OutputModel()
                {
                    Success = true,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new OutputModel()
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> UpdateBookingAfterCustomerPayment(int bookingId, CancellationToken cancellationToken)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            try
            {
                var bookingStatusHistory = new BookingStatusHistory()
                {
                    Booking = booking,
                    BookingStatus = BookingStatus.Active,
                    DateCreated = DateTime.UtcNow
                };

                _context.BookingStatusHistories.Add(bookingStatusHistory);
                await _context.SaveChangesAsync(cancellationToken);

                var customerData = _userManagementFactory.GetUsersAdditionalData(booking.HeadProposal.Project.Customer.UserName, UserRoleDefaults.CustomerRoleName).FirstOrDefault();
                customerData.TotalSpent += booking.TotalPrice;
                _context.UserRoleDatas.Update(customerData);

                if (customerData != null && !customerData.PaymentVerified)
                {
                    customerData.PaymentVerified = true;
                    _context.UserRoleDatas.Update(customerData);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"Project '{booking.HeadProposal.Project.ProjectTopic}' is now active." });
                await _context.SaveChangesAsync(cancellationToken);

                var notificationMessage = $"Project '{booking.HeadProposal.Project.ProjectTopic}' is now active.";
                var detailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id);
                var adminMessage = $"Customer made payment for project '{booking.HeadProposal.Project.ProjectTopic}'.";
                var notificationType = NotificationType.ActiveProject;

                var notifications = await _notificationService.SendNotifications(cancellationToken, booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, booking.HeadProposal.GHWId, booking.HeadProposal.Project.CustomerId);
                _notificationService.AddSidePanelNotifications(ref notifications, booking, EventType.Change, PanelTab.Chat);

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = true,
                    Message = string.Empty,
                    AdditionalInformation = notifications
                };
            }
            catch(Exception ex)
            {
                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }

        public bool CustomerPaymentCanBeMade(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            if (booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus != BookingStatus.Inactive)
                throw new Exception($"Booking is not in inactive state.");

            var customerPaymentTransaction = _context.Transactions.Where(x => x.Booking.Id == bookingId && x.IsSuccessful && x.PaymentType == PaymentType.PaymentToSystem).FirstOrDefault();

            if (customerPaymentTransaction != null)
                throw new Exception($"A payment cannot be made because the payment by customer is already made.");

            return true;
        }

        public bool CustomerRefundCanBeMade(int bookingId, decimal refundAmount)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            if (!BookingStatusGroups.Closed.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus))
                throw new Exception($"A refund cannot be made because the both parties haven't confirmed the project.");

            var transaction = booking.Transactions.Where(x => x.PaymentType == PaymentType.PaymentToSystem).FirstOrDefault();

            if (transaction == null)
                throw new Exception($"There's no payment for this booking.");

            if (transaction.TotalAmount < refundAmount)
                throw new Exception($"A refund cannot be made because the refund amount is bigger than the amount paid by the customer.");

            return true;
        }

        public decimal GetBookingCharges(int userId, int bookingId)
        {
            var customer = _userManagementFactory.FindUserById(userId);

            if (customer == null)
                throw new NotFoundException($"User {userId} not found.");

            if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User {customer.UserName} is cannot make a payment.");

            var booking = _context.Bookings.Find(bookingId);

            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            if (booking.HeadProposal.Project.Customer.UserName != customer.UserName)
                throw new AuthorizationException($"User is unauthorized to make payment for this booking.");

            var chargingAmount = booking.TotalPrice;

            return chargingAmount;
        }

        public string GetCustomerBookingPaymentTransactionId(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking == null)
                throw new NotFoundException($"Booking {bookingId} not found.");

            var transaction = booking.Transactions.Where(x => x.PaymentType == PaymentType.PaymentToSystem).FirstOrDefault();

            if (transaction == null)
                throw new Exception($"Transaction is not found");

            return transaction.TransactionId;
        }
    }
}
