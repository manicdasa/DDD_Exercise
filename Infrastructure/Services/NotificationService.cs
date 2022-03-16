using AutoMapper;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Defaults;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public NotificationService(IApplicationDbContext context, IMapper mapper, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public PagedList<NotificationDTO> GetUserNotifications(int userid, LookupInputModel model)
        {
            var query = _context.Notifications.Where(x => x.Receiver.Id == userid).OrderByDescending(x => x.DateTimeCreated)
                                        .ProjectTo<NotificationDTO>(_mapper.ConfigurationProvider);

            if (!string.IsNullOrWhiteSpace(model.Search))
                query = query.Where(x => x.Message.Contains(model.Search));

            if (model.Page != default || model.PageSize != default)
            {
                var a = new PagedList<NotificationDTO>(query, model.Page, model.PageSize);
                a.Items.Reverse();
                return a;
            }   
            else
                return new PagedList<NotificationDTO>(query);
        }

        public async Task<int> SendNotificationToReceivers(NotificationDTO notification, List<ApplicationUser> receivers, CancellationToken cancellationToken)
        {
            try
            {
                List<Notification> notifications = new List<Notification>();
                foreach (var receiver in receivers)
                {
                    notifications.Add(new Notification()
                    {
                        Message = notification.Message,
                        NotificationType = notification.NotificationType,
                        DetailsLink = notification.DetailsLink,
                        AuthorUsername = notification.AuthorUsername,
                        CustomerUsername = notification.CustomerUsername,
                        HeadProposalId = notification.HeadProposalId,
                        ProjectTopic = notification.ProjectTopic,
                        ProjectId = notification.ProjectId,
                        ProposalId = notification.ExactEntityId,
                        DateTimeCreated = DateTime.UtcNow,
                        IsSeen = false,
                        Receiver = receiver
                    });
                }

                _context.Notifications.AddRange(notifications);

                var res = await _context.SaveChangesAsync(cancellationToken);

                return res;
            }
            catch(Exception ex)
            {
                return 0;
            }
        
        }

        public async Task<int> SendNotificationToReceivers(List<NotificationSignalRDTO> notificationDTOs, CancellationToken cancellationToken)
        {
            List<Notification> notifications = new List<Notification>();
            try
            {
                foreach (var notificationDTO in notificationDTOs)
                {
                    var user = _userManagementFactory.FindUserById(notificationDTO.ReceiverUserId);

                    if (user != null)
                        notifications.Add(new Notification()
                        {
                            Message = notificationDTO.NotificationDTO.Message,
                            NotificationType = notificationDTO.NotificationDTO.NotificationType,
                            DetailsLink = notificationDTO.NotificationDTO.DetailsLink,
                            AuthorUsername = notificationDTO.NotificationDTO.AuthorUsername,
                            CustomerUsername = notificationDTO.NotificationDTO.CustomerUsername,
                            ProjectTopic = notificationDTO.NotificationDTO.ProjectTopic,
                            ProjectId = notificationDTO.NotificationDTO.ProjectId,
                            ProposalId = notificationDTO.NotificationDTO.ExactEntityId,
                            HeadProposalId = notificationDTO.NotificationDTO.HeadProposalId,
                            DateTimeCreated = DateTime.UtcNow,
                            IsSeen = false,
                            Receiver = user
                        });
                }

                _context.Notifications.AddRange(notifications);

                var res = await _context.SaveChangesAsync(cancellationToken);

                return res;
            }
           catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<NotificationSignalRDTO>> SendNotifications(CancellationToken cancellationToken, int proposalId, string message, string link, NotificationType notificationType, bool notifyAdmin = false, string adminMessage = "", params int[] receiverIds)
        {
            try
            {
                var proposal = _context.Proposals.Find(proposalId);

                List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>();

                foreach (var receiverId in receiverIds)
                {
                    var notification = new NotificationSignalRDTO()
                    {
                        ReceiverUserId = receiverId,
                        NotificationDTO = new NotificationDTO()
                        {
                            DateTimeCreated = DateTime.UtcNow,
                            IsSeen = false,
                            Message = message,
                            NotificationType = notificationType,
                            AuthorUsername = proposal == null ? string.Empty : proposal.HeadProposal.Ghostwriter.UserName,
                            CustomerUsername = proposal == null ? string.Empty : proposal.HeadProposal.Project.Customer.UserName,
                            ProjectTopic = proposal == null ? string.Empty : proposal.HeadProposal.Project.ProjectTopic,
                            ExactEntityId = proposal == null ? 0 : proposalId,
                            ProjectId = FileSystemDefaults.GetProjectIdFromLink(link),
                            DetailsLink = link,
                            HeadProposalId = proposal == null ? proposalId : proposal.HeadProposal.Id
                        }
                    };
                    notifications.Add(notification);
                }

                if (notifyAdmin)
                {
                    foreach (var admin in _userManagementFactory.GetAllAdminUsers())
                    {
                        var notificationAdmin = new NotificationSignalRDTO()
                        {
                            ReceiverUserId = admin.Id,
                            NotificationDTO = new NotificationDTO()
                            {
                                DateTimeCreated = DateTime.UtcNow,
                                IsSeen = false,
                                Message = adminMessage == string.Empty ? message : adminMessage,
                                AuthorUsername = proposal == null ? string.Empty : proposal.HeadProposal.Ghostwriter.UserName,
                                CustomerUsername = proposal == null ? string.Empty : proposal.HeadProposal.Project.Customer.UserName,
                                ProjectTopic = proposal == null ? string.Empty : proposal.HeadProposal.Project.ProjectTopic,
                                ExactEntityId = proposal == null ? 0 : proposalId,
                                ProjectId = FileSystemDefaults.GetProjectIdFromLink(link),
                                NotificationType = notificationType,
                                DetailsLink = link,
                                HeadProposalId = proposal == null ? proposalId : proposal.HeadProposal.Id
                            }
                        };
                        notifications.Add(notificationAdmin);
                    }
                }

                await SendNotificationToReceivers(notifications, cancellationToken);

                return notifications;
            }
           catch(Exception ex)
            {
                return new List<NotificationSignalRDTO>();
            }
        }

        public async Task<OutputModel> MarkNotificationsAsSeen(NotificationType notificationType, string username, CancellationToken cancellationToken)
        {
            try
            {
                var notifications = _context.Notifications.Where(x => x.Receiver.UserName == username && x.NotificationType == notificationType).ToList();

                notifications.ForEach(x => x.IsSeen = true);

                _context.Notifications.UpdateRange(notifications);

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
                    Message = ex.Message
                };
            }
        }

        public void AddSidePanelNotifications(ref List<NotificationSignalRDTO> notifications, object objectToMap, EventType eventType, PanelTab panelTab, params int[] additionalReceiverIds)
        {
            var panelObjectDTO = CreatePanelObject(objectToMap, eventType, panelTab);

            foreach (var notification in notifications)
            {
                notification.PanelObjectDTO = panelObjectDTO;
            }

            foreach (var receiverId in additionalReceiverIds ?? new int[] { })
            {
                notifications.Add(new NotificationSignalRDTO()
                {
                    PanelObjectDTO = panelObjectDTO,
                    ReceiverUserId = receiverId
                });
            }
        }

        public List<NotificationSignalRDTO> AddSidePanelNotifications(object objectToMap, EventType eventType, PanelTab panelTab, params int[] additionalReceiverIds)
        {
            List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>();

            var panelObjectDTO = CreatePanelObject(objectToMap, eventType, panelTab);

            foreach (var notification in notifications)
            {
                notification.PanelObjectDTO = panelObjectDTO;
            }

            foreach (var receiverId in additionalReceiverIds ?? new int[] { })
            {
                notifications.Add(new NotificationSignalRDTO()
                {
                    PanelObjectDTO = panelObjectDTO,
                    ReceiverUserId = receiverId
                });
            }

            return notifications;
        }

        private PanelObjectDTO CreatePanelObject(object objectToMap, EventType eventType, PanelTab panelTab)
        {
            object entity;

            if (panelTab == PanelTab.Chat)
                entity = _mapper.Map<BookingChatInfoDTO>(objectToMap);
            else
                entity = _mapper.Map<ProposalDTO>(objectToMap);

            var panelObjectDTO = new PanelObjectDTO()
            {
                EventType = eventType,
                PanelTab = panelTab,
                PanelObject = entity
            };

            return panelObjectDTO;
        }
    }
}
