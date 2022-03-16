using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Lookup.Queries;
using GhostWriter.Application.Lookup.Commands;
using GhostWriter.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.DTOs;
using System;

namespace GhostWriter.WebUI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LookupController : ApiControllerBase
    {
        #region Post methods

        [HttpPost]
        [Route(nameof(AddCustomExpertiseArea))]
        //[Authorize(Roles = "Customer,Ghostwriter")]
        public async Task<OutputModel> AddCustomExpertiseArea(string value)
        {
            var command = new AddCustomFieldCommand()
            {
                CustomFieldValue = value,
                Entity = new ExpertiseArea(),
            };

            var result = await Mediator.Send(command);

            if (result.Success)
                return result;
            else
                throw new Exception(result.Message);
        }

        [HttpPost]
        [Route(nameof(AddCustomKindOfWork))]
        //[Authorize(Roles = "Customer,Ghostwriter")]
        public async Task<OutputModel> AddCustomKindOfWork(string value)
        {
            var command = new AddCustomFieldCommand()
            {
                CustomFieldValue = value,
                Entity = new KindOfWork(),
            };

            var result = await Mediator.Send(command);

            if (result.Success)
                return result;
            else
                throw new Exception(result.Message);
            
        }

        #endregion

        #region Put methods

        [HttpPut]
        [Route(nameof(ProcessCustomKindOfWork))]
        [Authorize(Roles = "Admin")]
        public async Task<OutputModel> ProcessCustomKindOfWork(int id, bool approved)
        {
            var command = new EditCustomFieldCommand()
            {
                Entity = new KindOfWork(),
                EntityId = id,
                FieldStatus = approved ? FieldStatus.Approved : FieldStatus.Rejected
            };

            var result = await Mediator.Send(command);

            if (result.Success)
                return result;
            else
                throw new Exception(result.Message);
        }

        [HttpPut]
        [Route(nameof(ProcessCustomExpertiseArea))]
        [Authorize(Roles = "Admin")]
        public async Task<OutputModel> ProcessCustomExpertiseArea(int id, bool approved)
        {
            var command = new EditCustomFieldCommand()
            {
                Entity = new ExpertiseArea(),
                EntityId = id,
                FieldStatus = approved ? FieldStatus.Approved : FieldStatus.Rejected
            };

            var result = await Mediator.Send(command);

            if (result.Success)
                return result;
            else
                throw new Exception(result.Message);
        }

        #endregion

        #region Get methods

        [HttpGet]
        [Route(nameof(GetCustomPendingExpertiseAreas))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PagedList<ExpertiseAreaDTO>>> GetCustomPendingExpertiseAreas([FromQuery] GetCustomPendingExpertiseAreaQuery request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(GetCustomPendingKindsOfWork))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PagedList<KindOfWorkDTO>>> GetCustomPendingKindsOfWork([FromQuery] GetCustomPendingKindOfWorkQuery request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("KindOfWork")]
        public async Task<IActionResult> KindOfWork([FromQuery] GetKindOfWorkQuery request)
        {
            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        [HttpGet]
        [Route("AreaOfExpertise")]
        public async Task<IActionResult> AreaOfExpertise([FromQuery] GetAreaOfExpertiseQuery request)
        {
            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        [HttpGet]
        [Route("Language")]
        public async Task<IActionResult> Language([FromQuery] GetLanguageQuery request)
        {
            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        [HttpGet]
        [Route("Degree")]
        public async Task<IActionResult> Degree([FromQuery] GetDegreeQuery request)
        {
            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        #endregion
    }
}
