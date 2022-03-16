using AutoMapper;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.User.Commands.UpdateUser;
using GhostWriter.Application.User.Queries;
using GhostWriter.Domain.Defaults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Edits the private information of the currently logged author.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(EditAuthorPrivateInfo))]
        [ProducesResponseType(typeof(OutputModel), 200)]
        [Authorize]
        public async Task<IActionResult> EditAuthorPrivateInfo(EditAuthorPrivateInfoCommand command)
        {
            EditAuthorPrivateInfoCommandExtended request = (EditAuthorPrivateInfoCommandExtended)_mapper.Map(command, typeof(EditAuthorPrivateInfoCommand), typeof(EditAuthorPrivateInfoCommandExtended));
            request.GHWUsername = User.FindFirst(ClaimTypes.Name).Value;

            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        /// <summary>
        /// Edits the private information of the currently logged author.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(ChangeAuthorsProfilePicture))]
        [ProducesResponseType(typeof(OutputModel), 200)]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<IActionResult> ChangeAuthorsProfilePicture(ChangeAuthorsProfilePictureCommand command)
        {
            ChangeAuthorsProfilePictureCommandExtended request = new ChangeAuthorsProfilePictureCommandExtended()
            {
                GHWUsername = User.FindFirst(ClaimTypes.Name).Value,
                Picture = command.Picture
            }; 

            request.GHWUsername = User.FindFirst(ClaimTypes.Name).Value;

            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        /// <summary>
        /// Anonymize customer
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(AnonymizeCustomer))]
        [ProducesResponseType(typeof(OutputModel), 200)]
        public async Task<IActionResult> AnonymizeCustomer()
        {
            AnonymizeUserCommand request = new AnonymizeUserCommand()
            {
                RoleName = UserRoleDefaults.CustomerRoleName,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        /// <summary>
        /// Anonymize author
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(AnonymizeAuthor))]
        [ProducesResponseType(typeof(OutputModel), 200)]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<IActionResult> AnonymizeAuthor()
        {
            AnonymizeUserCommand request = new AnonymizeUserCommand()
            {
                RoleName = UserRoleDefaults.GhostwriterRoleName,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
        }

        /// <summary>
        /// Anonymize customer check
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route(nameof(AnonymizeCustomerCheck))]
        [ProducesResponseType(typeof(OutputModel), 200)]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AnonymizeCustomerCheck()
        {
            AnonymizeUserCheckCommand request = new AnonymizeUserCheckCommand()
            {
                RoleName = UserRoleDefaults.CustomerRoleName,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Anonymize author check
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route(nameof(AnonymizeAuthorCheck))]
        [ProducesResponseType(typeof(OutputModel), 200)]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<IActionResult> AnonymizeAuthorCheck()
        {
            AnonymizeUserCheckCommand request = new AnonymizeUserCheckCommand()
            {
                RoleName = UserRoleDefaults.GhostwriterRoleName,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var result = await Mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Gets the suitable authors according to the search parameters.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(AuthorsSearch))]
        [ProducesResponseType(typeof(PagedList<AuthorDTO>), 200)]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AuthorsSearch([FromQuery] SearchAuthorsQuery request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Gets the private information of the currently logged author.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAuthorPrivateInfo))]
        [ProducesResponseType(typeof(AuthorPrivateInfoDTO), 200)]
        [Authorize]
        public async Task<IActionResult> GetAuthorPrivateInfo()
        {
            int ghwid;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out ghwid);

            GetAuthorPrivateInfoQuery request = new GetAuthorPrivateInfoQuery { GHWId = ghwid };

            var result = await Mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Gets the private information of the currently logged author.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAuthorPublicInfo))]
        [ProducesResponseType(typeof(AuthorPublicInfoDTO), 200)]
        public async Task<IActionResult> GetAuthorPublicInfo(int authorId)
        {
            GetAuthorPrivateInfoQuery request = new GetAuthorPrivateInfoQuery { GHWId = authorId };

            var result = await Mediator.Send(request);

            AuthorPublicInfoDTO retVal = (AuthorPublicInfoDTO)_mapper.Map(result, typeof(AuthorPrivateInfoDTO), typeof(AuthorPublicInfoDTO));

            return Ok(retVal);
        }

        /// <summary>
        /// Gets the private information of the currently logged customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetCustomerPrivateInfo))]
        [ProducesResponseType(typeof(CustomerPrivateInfoDTO), 200)]
        [Authorize]
        public async Task<IActionResult> GetCustomerPrivateInfo()
        {
            int customerid;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out customerid);

            GetCustomerPrivateInfoQuery request = new GetCustomerPrivateInfoQuery { CustomerId = customerid };

            var result = await Mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Gets the private information of the currently logged customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetCustomerPublicInfo))]
        [ProducesResponseType(typeof(CustomerPublicInfoDTO), 200)]
        [Authorize]
        public async Task<IActionResult> GetCustomerPublicInfo(int customerId)
        {
            GetCustomerPrivateInfoQuery request = new GetCustomerPrivateInfoQuery { CustomerId = customerId };

            var result = await Mediator.Send(request);

            CustomerPublicInfoDTO retVal = (CustomerPublicInfoDTO)_mapper.Map(result, typeof(CustomerPrivateInfoDTO), typeof(CustomerPublicInfoDTO));

            return Ok(retVal);
        }
    }
}
