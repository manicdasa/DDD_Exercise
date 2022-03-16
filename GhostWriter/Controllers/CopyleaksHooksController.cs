using AutoMapper;
using Braintree;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.User.Commands.UpdateUser;
using GhostWriter.Application.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Copyleaks.SDK.V3.API.Models.Callbacks;
using GhostWriter.Application.Booking.Commands.CopyleaksCheckDataSave;
using GhostWriter.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;
using GhostWriter.Application.Defaults;
using System.Linq;
using System;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CopyleaksHooksController : ApiControllerBase
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public CopyleaksHooksController(IHubContext<NotificationHub> hubContext)
        {
            _notificationHubContext = hubContext;
        }

        /// <summary>
        /// Edits the private information of the currently logged author.
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [Route("Completed")]
        public async Task<ActionResult> Completed([FromRoute] string scanId, [FromBody] CompletedCallback model)
        {
            CopyleaksCheckDataSaveCommand command = new CopyleaksCheckDataSaveCommand()
            {
                ScanId = model.ScannedDocument.ScanId,
                TotalExcluded = model.ScannedDocument.TotalExcluded,
                TotalWordsScanned = model.ScannedDocument.TotalWords,
                AggregatedScore = model.Results.Score.AggregatedScore,
                IdenticalWords = model.Results.Score.IdenticalWords,
                MinorChangedWords = model.Results.Score.MinorChangedWords,
                RelatedMeaningWords = model.Results.Score.RelatedMeaningWords,
                Status = model.Status.ToString(),
                Credits = model.ScannedDocument.Credits,
                CreationTime = model.ScannedDocument.CreationTime
            };

            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return Ok();
            }
            else
            {
                throw new Exception(result.Message);
            }

        }
    }
}
