using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Booking.Commands.CreateBooking;
using GhostWriter.Application.Booking.Commands.GetBookings;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using GhostWriter.Application.Booking.Commands.UploadProjectDocument;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using GhostWriter.Application.Booking.Commands.AddReview;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Booking.Commands.Disputes;
using System;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Defaults;
using GhostWriter.Application.Project.Commands;
using Microsoft.AspNetCore.SignalR;
using GhostWriter.WebUI.Hubs;
using System.Threading;
using GhostWriter.Infrastructure.Settings;
using System.Linq;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Customer,Ghostwriter")]
    public class BookingController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IPayoutService _payoutService;
        private readonly IBraintreeService _braintreeService;
        private readonly EnvironmentConfigSettings _environmentConfig;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public BookingController(IMapper mapper, IHttpContextAccessor accessor, IPayoutService payoutService, IHubContext<NotificationHub> hubContext, EnvironmentConfigSettings environmentConfigSettings, IBraintreeService braintreeService)
        {
            _mapper = mapper;
            _accessor = accessor;
            _payoutService = payoutService;
            _notificationHubContext = hubContext;
            _braintreeService = braintreeService;
            _environmentConfig = environmentConfigSettings;

        }

        /// <summary>
        /// Customer accepts bid. Booking is created which is in inactive status.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(AcceptBid))]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OutputModel>> AcceptBid(int proposalId)
        {
            AcceptProposalCreateBookingCommand command = new AcceptProposalCreateBookingCommand()
            {
                ProposalId = proposalId,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                RoleName = UserRoleDefaults.CustomerRoleName
            };

            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }

        }

        /// <summary>
        /// Author accepts offer. Booking is created which is in inactive status.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(AcceptOffer))]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<ActionResult<OutputModel>> AcceptOffer(int proposalId)
        {
            AcceptProposalCreateBookingCommand command = new AcceptProposalCreateBookingCommand()
            {
                ProposalId = proposalId,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                RoleName = UserRoleDefaults.GhostwriterRoleName
            };

            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        /// <summary>
        /// Author or customer of the project uploads the project document
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(UploadProjectDocument))]
        [Authorize(Roles = "Ghostwriter,Customer")]
        public async Task<ActionResult<OutputModel>> UploadProjectDocument(int bookingId, IFormFile formFile)
        {
            UploadProjectDocumentCommand command = new UploadProjectDocumentCommand()
            {
                BookingId = bookingId,
                FormFile = formFile,
                IsFinalVersion = false,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        /// <summary>
        /// Uploads the project document
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(SubmitFinalVersion))]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<ActionResult<OutputModel>> SubmitFinalVersion(int bookingId, IFormFile formFile)
        {
            UploadProjectDocumentCommand command = new UploadProjectDocumentCommand()
            {
                BookingId = bookingId,
                FormFile = formFile,
                IsFinalVersion = true,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                CalbackUrl = (_environmentConfig.Dev) ? "https://89.216.25.4:45455/CopyleaksHooks" : BuildCallbackUrl("CopyleaksHooks")
            };

            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        /// <summary>
        /// Adds a review to a completed project
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(nameof(AddProjectReview))]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OutputModel>> AddProjectReview(AddReviewCommand command)
        {
            AddReviewCommandExtended request = new AddReviewCommandExtended()
            {
                BookingId = command.BookingId,
                Comment = string.Empty,
                StarRating = command.StarRating,
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(request);

            if(result.Success)
            { 
                result.AdditionalInformation.ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        /// <summary>
        /// Project gets confirmed by the customer. 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(ConfirmProject))]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OutputModel>> ConfirmProject(int bookingId)
        {
            ConfirmProjectCommand command = new ConfirmProjectCommand()
            {
                BookingId = bookingId,
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        /// <summary>
        /// Customer creates dispute for an active project
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateDispute))]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OutputModel>> CreateDispute(int bookingId, string message)
        {
            CreateDisputeCommand request = new CreateDisputeCommand()
            {
                BookingId = bookingId,
                Message = message,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(request);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        /// <summary>
        /// Admin accepts a dispute of a project
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(AcceptDispute))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OutputModel>> AcceptDispute(int bookingId, decimal refundAmount,decimal paymentAmountAuthor, string message, CancellationToken cancellationToken)
        {
            int userid;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userid);

            var retVal1 = await Mediator.Send(new ResolveDisputeCommand()
            {
                BookingId = bookingId,
                Message = message,
                RefundAmount = refundAmount,
                PaymentAmountAuthor = paymentAmountAuthor,
                NewDisputeStatus = Domain.Enums.DisputeStatus.Accepted,
                Username = User.FindFirst(ClaimTypes.Name).Value
            });

            if (!retVal1.Success)
                throw new Exception(retVal1.Message);
                //return new OutputModel()
                //{
                //    Success = retVal1.Success,
                //    Message = retVal1.Message
                //};

            retVal1.AdditionalInformation.ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));

            ConfirmProjectCommand command1 = new ConfirmProjectCommand()
            {
                BookingId = bookingId,
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value
            };

            var retVal2 = await Mediator.Send(command1);

            if (retVal2.Success)
            {
                retVal2.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                retVal2.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
            }

            //TODO: Transfer the money to the author and customer
            var result = await _braintreeService.MakeRefund(userid, bookingId, refundAmount, cancellationToken);

            if (!result.Success)
                throw new Exception(result.Message);

            return new OutputModel()
            {
                Success = result.Success,
                Message = result.Message
            };
        }

        /// <summary>
        /// Admin declines a dispute of the project
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(DeclineDispute))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OutputModel>> DeclineDispute(int bookingId, string message)
        {
            var retVal1 = await Mediator.Send(new ResolveDisputeCommand()
            {
                BookingId = bookingId,
                Message = message,
                NewDisputeStatus = Domain.Enums.DisputeStatus.Declined,
                Username = User.FindFirst(ClaimTypes.Name).Value
            });

            if (!retVal1.Success)
                throw new Exception(retVal1.Message);
            //return new OutputModel()
            //{
            //    Success = retVal1.Success,
            //    Message = retVal1.Message
            //};

            ConfirmProjectCommand command1 = new ConfirmProjectCommand()
            {
                BookingId = bookingId,
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value
            };

            var retVal2 = await Mediator.Send(command1);

            if (retVal2.Success)
            {
                retVal2.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                retVal2.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
            }
            //TODO: Transfer the money to the author
            if (!retVal2.Success)
                throw new Exception(retVal2.Message);

            return new OutputModel()
            {
                Success = retVal2.Success,
                Message = retVal2.Message
            };
        }

        /// <summary>
        /// Project gets cancelled by the customer. 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(nameof(CancelProject))]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OutputModel>> CancelProject(int bookingId)
        {
            CancelProjectCommand cancelCommand = new CancelProjectCommand()
            {
                BookingId = bookingId,
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value
            };

            var cancelResult = await Mediator.Send(cancelCommand);

            if (!cancelResult.Success)
                throw new Exception(cancelResult.Message);

            DeleteProjectCommand deleteCommand = new DeleteProjectCommand()
            {
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value,
                ProjectId = cancelResult.AdditionalInformation.HeadProposal.Project.Id
            };

            var result = await Mediator.Send(deleteCommand);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }

        }

        #region Get methods

        /// <summary>
        /// Gets author's closed projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorsClosedProjects))]
        public async Task<ActionResult<PagedList<BookingShortInfoDTO>>> GetAuthorsClosedProjects([FromQuery] GetAuthorBookingsQuery query)
        {
            var request = (GetBookingsQueryExtended)_mapper.Map(query, typeof(GetAuthorBookingsQuery), typeof(GetBookingsQueryExtended));

            request.Username = User.FindFirst(ClaimTypes.Name).Value;
            request.RoleName = UserRoleDefaults.GhostwriterRoleName;
            request.BookingStatuses = BookingStatusGroups.Closed;

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets author's active projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorsActiveProjects))]
        public async Task<ActionResult<PagedList<BookingShortInfoDTO>>> GetAuthorsActiveProjects([FromQuery] GetAuthorBookingsQuery query)
        {
            var request = (GetBookingsQueryExtended)_mapper.Map(query, typeof(GetAuthorBookingsQuery), typeof(GetBookingsQueryExtended));

            request.Username = User.FindFirst(ClaimTypes.Name).Value;
            request.RoleName = UserRoleDefaults.GhostwriterRoleName;
            request.BookingStatuses = BookingStatusGroups.Open;

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets customer's closed projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersClosedProjects))]
        public async Task<ActionResult<PagedList<BookingShortInfoDTO>>> GetCustomersClosedProjects([FromQuery] GetCustomerBookingsQuery query)
        {
            var request = (GetBookingsQueryExtended)_mapper.Map(query, typeof(GetCustomerBookingsQuery), typeof(GetBookingsQueryExtended));

            request.Username = User.FindFirst(ClaimTypes.Name).Value;
            request.RoleName = UserRoleDefaults.CustomerRoleName;
            request.BookingStatuses = BookingStatusGroups.Closed;

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets customer's active projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersActiveProjects))]
        public async Task<ActionResult<PagedList<BookingShortInfoDTO>>> GetCustomersActiveProjects([FromQuery] GetCustomerBookingsQuery query)
        {
            var request = (GetBookingsQueryExtended)_mapper.Map(query, typeof(GetCustomerBookingsQuery), typeof(GetBookingsQueryExtended));

            request.Username = User.FindFirst(ClaimTypes.Name).Value;
            request.RoleName = UserRoleDefaults.CustomerRoleName;
            request.BookingStatuses = BookingStatusGroups.Open;

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets customer's booking short info and it's last chat message 
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetBookingChatInfoCustomer))]
        public async Task<ActionResult<PagedList<BookingChatInfoDTO>>> GetBookingChatInfoCustomer()
        {
            var request = new GetBookingChatInfoQuery()
            {
                RoleName = UserRoleDefaults.CustomerRoleName,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets author's booking short info and it's last chat message 
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetBookingChatInfoAuthor))]
        public async Task<ActionResult<PagedList<BookingChatInfoDTO>>> GetBookingChatInfoAuthor()
        {
            var request = new GetBookingChatInfoQuery()
            {
                RoleName = UserRoleDefaults.GhostwriterRoleName,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets Booking Details
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetBookingDetails))]
        public async Task<ActionResult<BookingDetailsDTO>> GetBookingDetails(int bookingId)
        {
            GetBookingDetailsQuery request = new GetBookingDetailsQuery()
            {
                BookingId = bookingId,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Downloads the project document
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(nameof(DownloadProjectDocument))]

        public async Task<ActionResult> DownloadProjectDocument(int documentId)
        {
            GetBookingDocumentQuery request = new GetBookingDocumentQuery()
            {
                Username = User.FindFirst(ClaimTypes.Name).Value,
                DocumentId = documentId
            };

            var document = await Mediator.Send(request);

            var filePath = Path.Combine(document.LocalPath, document.PrivateName);

            if (!System.IO.File.Exists(filePath))
                return null;
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(filePath, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(memory, contentType);
        }

        #endregion

        private string BuildCallbackUrl(string controllerName)
        {
            string scheme = _accessor.HttpContext.Request.Scheme;
            string host = _accessor.HttpContext.Request.Host.Host;
            int port = _accessor.HttpContext.Request.Host.Port ?? 443;

            UriBuilder uriBuilder = new UriBuilder(scheme, host, port, controllerName);

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
