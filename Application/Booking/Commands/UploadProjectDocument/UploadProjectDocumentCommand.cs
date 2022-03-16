using System;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using System.Linq;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;
using System.Transactions;
using GhostWriter.Domain.Entities;
using Microsoft.AspNetCore.Http;
using GhostWriter.Application.Defaults;
using System.IO;
using System.Collections.Generic;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Booking.Commands.UploadProjectDocument
{
    public class UploadProjectDocumentCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public int BookingId { get; set; }
        public IFormFile FormFile { get; set; }
        public bool IsFinalVersion { get; set; }
        public string Username { get; set; }
        public string CalbackUrl { get; set; }
    }

    public class UploadProjectDocumentCommandHandler : IRequestHandler<UploadProjectDocumentCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IFileProvider _fileProvider;
        private readonly IPlagiarismChecker _plagiarismChecker;
        private readonly INotificationService _notificationService;

        public UploadProjectDocumentCommandHandler(IApplicationDbContext context, INotificationService notificationService, IUserManagementFactory userManagementFactory, IFileProvider fileProvider, IPlagiarismChecker plagiarismChecker)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _fileProvider = fileProvider;
            _plagiarismChecker = plagiarismChecker;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(UploadProjectDocumentCommand request, CancellationToken cancellationToken)
        {
            if (request.FormFile == null)
                throw new NotFoundException($"File is not found or is not correctly sent to the server.");

            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to accept an offer.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"User {request.Username} not found.");

            var booking = _context.Bookings.Find(request.BookingId);

            if (booking is null)
                throw new NotFoundException($"Booking {request.BookingId} not found.");

            if (booking.HeadProposal.GHWId != user.Id && booking.HeadProposal.Project.CustomerId != user.Id)
                throw new AuthorizationException($"User {request.Username} is unauthorized to upload a document for this project.");

            if (BookingStatusGroups.Closed.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus))
                throw new NotFoundException($"A document cannot be uploaded because booking {request.BookingId} is already closed.");

            try
            {
                var privateName = Path.GetFileNameWithoutExtension(request.FormFile.FileName) + _fileProvider.GetFileNameExtensionGuid().ToString() + Path.GetExtension(request.FormFile.FileName);
                var path = FileSystemDefaults.BookingDocumentsLocalPath + request.BookingId;
                var fullPath = Path.Combine(path, privateName);

                if (!await _fileProvider.SaveFile(request.FormFile, path, privateName))
                    return new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Message = "Error while saving the document",
                        Success = false
                    };

                Document newDoc = new Document()
                {
                    BookingId = booking.Id,
                    IsFinalVersion = request.IsFinalVersion,
                    DateCreated = DateTime.UtcNow,
                    LocalPath = path,
                    PublicName = request.FormFile.FileName,
                    PrivateName = privateName,
                    UploadedByUser = user
                };

                booking.LastUpdate = DateTime.UtcNow;
                _context.Bookings.Update(booking);

                booking.Documents.Add(newDoc);
                await _context.SaveChangesAsync(cancellationToken);

                var notifications = await CreateNotificationsAndLogs(newDoc, request.IsFinalVersion, request.Username, cancellationToken);

                if (request.IsFinalVersion)
                {
                    if (!BookingStatusGroups.RequiredForFinalVersionState.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus))
                        throw new Exception("A booking needs to be active for a final version to be submitted.");

                    newDoc.CopyLeaksScanId = _plagiarismChecker.CreateCopyleaksScanId(newDoc.Id);
                    _context.Documents.Update(newDoc);
                    await _context.SaveChangesAsync(cancellationToken);                   

                    BookingStatusHistory newStatus = new BookingStatusHistory()
                    {
                        Booking = booking,
                        DateCreated = DateTime.UtcNow,
                        BookingStatus = BookingStatus.FinalVersionSubmitted
                    };

                    booking.BookingStatusHistories.Add(newStatus);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _plagiarismChecker.CheckForPlagiarismAsync(request.CalbackUrl, request.FormFile.FileName, fullPath, newDoc.CopyLeaksScanId.ToString());
                    notifications.AddRange(await CreateNotificationsAndLogsPlagiarismCheck(newDoc, cancellationToken));
                }

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Message = string.Empty,
                    Success = true,
                    AdditionalInformation = notifications
                };
            }
            catch (Exception ex)
            {
                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Message = ex.InnerException.Message,
                    Success = false
                };
            }
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Document document, bool isFinalVersion, string username, CancellationToken cancellationToken)
        {
            _context.Messages.Add(new Message() { Conversation = document.Booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{username} uploaded a document {(isFinalVersion ? "final version." : string.Empty)}." });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"A document {(isFinalVersion ? "final version" : string.Empty)} is uploaded for project '{document.Booking.HeadProposal.Project.ProjectTopic}' by {username}.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.BookingDetailsPath(document.Booking.Id, document.Booking.HeadProposal.Id);
            var notificationType = NotificationType.ActiveProject;
            int receiverId = username == document.Booking.HeadProposal.Ghostwriter.UserName ? document.Booking.HeadProposal.Project.CustomerId : document.Booking.HeadProposal.GHWId;

            var notifications = await _notificationService.SendNotifications(cancellationToken, document.Booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, isFinalVersion ? true : false, adminMessage, receiverId);
            _notificationService.AddSidePanelNotifications(ref notifications, document.Booking, EventType.Change, PanelTab.Chat, username == document.Booking.HeadProposal.Ghostwriter.UserName ? document.Booking.HeadProposal.GHWId : document.Booking.HeadProposal.Project.CustomerId);


            return notifications;
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogsPlagiarismCheck(Domain.Entities.Document document, CancellationToken cancellationToken)
        {
            _context.Messages.Add(new Message() { Conversation = document.Booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"Final version subbmitted for plagiarism check." });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"Final version for project '{document.Booking.HeadProposal.Project.ProjectTopic}' submitted for plagiarsm check";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.BookingDetailsPath(document.Booking.Id, document.Booking.HeadProposal.Id);
            var notificationType = NotificationType.ActiveProject;
            var notifications = await _notificationService.SendNotifications(cancellationToken, document.Booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, document.Booking.HeadProposal.GHWId, document.Booking.HeadProposal.Project.CustomerId);
            _notificationService.AddSidePanelNotifications(ref notifications, document.Booking, EventType.Change, PanelTab.Chat, document.Booking.HeadProposal.GHWId);


            return notifications;
        }
    }
}
