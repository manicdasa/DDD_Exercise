using AutoMapper;
using Braintree;
using GhostWriter.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using System.Threading;
using GhostWriter.Infrastructure.Settings;
using Microsoft.AspNetCore.SignalR;
using GhostWriter.WebUI.Hubs;
using System;
using GhostWriter.Application.Defaults;
using System.Linq;
using GhostWriter.Application.Common.Models;
using GhostWriter.Infrastructure.Services;
using System.Collections.Generic;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBraintreeGateway _braintreeGateway;
        private readonly IBookingPaymentService _bookingPaymentService;
        private readonly PayPalConfigSettings _payPalConfigSettings;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IBraintreeService _braintreeService;

        //private static Semaphore _semaphore;
        public PaymentController(IMapper mapper, IBraintreeGateway braintreeGateway, IBookingPaymentService bookingPaymentService, PayPalConfigSettings payPalConfigSettings, IHubContext<NotificationHub> hubContext, IBraintreeService braintreeService)
        {
            _mapper = mapper;
            _braintreeGateway = braintreeGateway;
            _bookingPaymentService = bookingPaymentService;
            _payPalConfigSettings = payPalConfigSettings;
            _notificationHubContext = hubContext;
            _braintreeService = braintreeService;
        }

        /// <summary>
        /// Gets the paypal business account's data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetPaypalCredentials))]
        public PaypalInfoDTO GetPaypalCredentials()
        {
            PaypalInfoDTO retVal = new PaypalInfoDTO()
            {
                MerchantId = _payPalConfigSettings.MerchantId,
                PaypalClientID = _payPalConfigSettings.PaypalClientID,
                Sandbox = _payPalConfigSettings.Sandbox
            };

            return retVal;
        }

        /// <summary>
        /// Edits the private information of the currently logged author.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GeneratePaymentToken))]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GeneratePaymentToken()
        {
            var clientToken = _braintreeGateway.ClientToken.Generate();

            return Ok(clientToken);
        }

        /// <summary>
        /// Makes refund to the customer (in the case of dispute)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(MakeRefund))]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> MakeRefund(int bookingId, decimal refundAmount, CancellationToken cancellationToken)
        {
           
                int userid;
                int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userid);

                //_semaphore = new Semaphore(0, 1, bookingId.ToString());

                object value = new object();

                var isAdded = ConcurrencyService.CheckBookingWithBookingId.TryAdd(bookingId, value);

                ExtendedOutputModelList<NotificationSignalRDTO> result;

                lock (ConcurrencyService.CheckBookingWithBookingId[bookingId])
                {
                    result = _braintreeService.MakeRefund(userid, bookingId, refundAmount, cancellationToken).Result;
                }

                var isRemoved = ConcurrencyService.CheckBookingWithBookingId.TryRemove(new KeyValuePair<int, object>(bookingId, value));

            //_semaphore.Release();

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);

        }

        /// <summary>
        /// Charge customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(MakePayment))]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<OutputModel> MakePayment(int headProposalId, string paymentMethodNonce, CancellationToken cancellationToken)
        {
			try
			{
                int customerid;
                int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out customerid);

                //_semaphore = new Semaphore(0, 1, headProposalId.ToString());

                object value = new object();

                var isAdded = ConcurrencyService.CheckBookingWithHeadProposal.TryAdd(headProposalId, value);

                ExtendedOutputModelList <NotificationSignalRDTO> result;

                lock (ConcurrencyService.CheckBookingWithHeadProposal[headProposalId])
                {
                    result = _braintreeService.MakePayment(customerid, headProposalId, paymentMethodNonce, cancellationToken).Result;
                }

                var isRemoved = ConcurrencyService.CheckBookingWithHeadProposal.TryRemove(new KeyValuePair<int, object>(headProposalId, value));


                //_semaphore.Release();

                if (result.Success)
                {
                    result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                    result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                    return new OutputModel()
                    {
                        Success = true,
                        Message = string.Empty
                    };
                }
                else
                {
                    throw new Exception(result.Message);
                }
			}
			catch(Exception ex)
			{
				throw ex;
			}
            
                
            
        }
    }
}
