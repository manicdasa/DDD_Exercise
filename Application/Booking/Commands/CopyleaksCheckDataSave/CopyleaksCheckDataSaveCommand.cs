using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Booking.Commands.CopyleaksCheckDataSave
{
    public class CopyleaksCheckDataSaveCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public string ScanId { get; set; }
        public uint Credits { get; set; }
        public DateTime CreationTime { get; set; }
        public uint TotalWordsScanned { get; set; }
        public uint TotalExcluded { get; set; }
        public uint IdenticalWords { get; set; }
        public uint MinorChangedWords { get; set; }
        public uint RelatedMeaningWords { get; set; }
        public double AggregatedScore { get; set; }
        public string Status { get; set; }
       
    }

    public class CopyleaksCheckDataSaveCommandHandler : IRequestHandler<CopyleaksCheckDataSaveCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public CopyleaksCheckDataSaveCommandHandler(IApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(CopyleaksCheckDataSaveCommand request, CancellationToken cancellationToken)
        {
            var document = _context.Documents.Where(x => x.CopyLeaksScanId == request.ScanId).FirstOrDefault();

            if(document is null)
                throw new NotFoundException($"Document with Id {request.ScanId} not found.");

            try
            {
                PlagiarismCheckInformation plagiarismCheckInformation = new PlagiarismCheckInformation()
                {
                    Document = document,
                    DocumentId = document.Id,
                    AggregatedScore = request.AggregatedScore,
                    Credits = request.Credits,
                    DateCreated = request.CreationTime,
                    Status = request.Status,
                    TotalExcluded = request.TotalExcluded,
                    TotalWordsScanned = request.TotalWordsScanned,
                    IdenticalWords = request.IdenticalWords,
                    MinorChangedWords = request.MinorChangedWords,
                    RelatedMeaningWords = request.RelatedMeaningWords
                };

                _context.PlagiarismCheckInformations.Add(plagiarismCheckInformation);
                await _context.SaveChangesAsync(cancellationToken);

                document.Booking.PlagueScanned = true;

                document.Booking.BookingStatusHistories.Add(new BookingStatusHistory()
                {
                    Booking = document.Booking,
                    BookingStatus = Domain.Enums.BookingStatus.PlagiarismCheckDone,
                    DateCreated = DateTime.UtcNow
                });

                _context.Bookings.Update(document.Booking);
                await _context.SaveChangesAsync(cancellationToken);

                BookingStatusHistory bookingStatus = new BookingStatusHistory()
                {
                    Booking = document.Booking,
                    BookingStatus = Domain.Enums.BookingStatus.PlagiarismCheckDone,
                    DateCreated = DateTime.UtcNow
                };

                _context.BookingStatusHistories.Add(bookingStatus);
                await _context.SaveChangesAsync(cancellationToken);

                var notifications = await CreateNotificationsAndLogs(document, request.AggregatedScore, cancellationToken);

                //TODO: SignalR - Notify customer and the author that the document plagiarism check is completed

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = true,
                    Message = string.Empty,
                    AdditionalInformation = notifications
                };
            }
            catch (Exception ex)
            {
                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Document document, double similarityScore, CancellationToken cancellationToken)
        {
            _context.Messages.Add(new Message() { Conversation = document.Booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"Plagiarism check for document '{document.PublicName}' done. Similarity Score: {similarityScore}%." });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"Plagiarism check for focument '{document.PublicName}' (project '{document.Booking.HeadProposal.Project.ProjectTopic}') is done. Similarity Score: {similarityScore}%.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.BookingDetailsPath(document.Booking.Id, document.Booking.HeadProposal.Id);
            var notificationType = NotificationType.ActiveProject;

            var notifications = await _notificationService.SendNotifications(cancellationToken, document.Booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, document.Booking.HeadProposal.GHWId, document.Booking.HeadProposal.Project.CustomerId);
            _notificationService.AddSidePanelNotifications(ref notifications, document.Booking, EventType.Change, PanelTab.Chat);

            return notifications;
        }
    }
}
