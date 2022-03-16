using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using IbanNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GhostWriter.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IPayoutService _payoutService;

        public AuthenticateController(IUserManagementFactory userManagementFactory, IPayoutService payoutService)
        {
            _userManagementFactory = userManagementFactory;
            _payoutService = payoutService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LogInResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var loginResponse = await _userManagementFactory.Login(model);

            if (loginResponse.Success)
                return Ok(loginResponse.SuccessPayload);
            else
                throw new Exception(loginResponse.Message);

            //return BadRequest(loginResponse);

        }

        [HttpPost]
        [Route("registerUser")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(BasicResponse), 400)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResponse = await _userManagementFactory.RegisterUser(model, cancellationToken);

            if (registerResponse.Success)
                return Ok(registerResponse);
            else
                throw new Exception(registerResponse.Message);
        }

        [HttpPost]
        [Route("registerAuthor")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(BasicResponse), 400)]
        public async Task<IActionResult> RegisterAuthor([FromBody] RegisterAuthorModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception($"Registration data is not valid");

                RegisterAuthorInputModel registerAuthorInputModel = new RegisterAuthorInputModel()
                {
                    DateOfBirth = model.DateOfBirth,
                    DirectBooking = model.DirectBooking,
                    Email = model.Email,
                    ExpertiseAreas = model.ExpertiseAreas,
                    FirstName = model.FirstName,
                    HighestDegreeId = model.HighestDegreeId,
                    IBAN = model.IBAN,
                    KindOfWorks = model.KindOfWorks,
                    LanguageIds = model.LanguageIds,
                    LastName = model.LastName,
                    PagesPerDay = model.PagesPerDay,
                    Password = model.Password,
                    PricePerPage = model.PricePerPage,
                    ProfileIntroduction = model.ProfileIntroduction,
                    ProfilePicture = model.ProfilePicture,
                    Username = model.Username
                };

                if (!string.IsNullOrWhiteSpace(model.PaypalCode))
                {
                    var paypalCreds = await _payoutService.GetAuthorsPaypalCredentials(model.PaypalCode);

                    if (!paypalCreds.Success && string.IsNullOrWhiteSpace(model.IBAN))
                        throw new Exception(paypalCreds.Message);

                    //TODO: we need to change this once the GetAuthorsPaypalCredentials method startes working
                    registerAuthorInputModel.PaypalEmail = paypalCreds.Message;
                    registerAuthorInputModel.PaypalPayerID = paypalCreds.Message;
                }
                else
                {
                    IIbanValidator validator = new IbanValidator();
                    ValidationResult validationResult = validator.Validate(model.IBAN);
                    if (!validationResult.IsValid)
                        throw new Exception($"IBAN is not valid.");
                }

                var registerResponse = await _userManagementFactory.RegisterAuthor(registerAuthorInputModel, cancellationToken);

                if (registerResponse.Success)
                    return Ok(registerResponse);
                else
                    throw new Exception(registerResponse.Message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpPost]
        [Route(nameof(GetAuthorsPaypalCredentials))]
        public async Task<IActionResult> GetAuthorsPaypalCredentials(string code)
        {
            var res = await _payoutService.GetAuthorsPaypalCredentials(code);

            return Ok(res);
        }

        [HttpPost]
        [Route(nameof(CheckEmailAvailability))]
        [ProducesResponseType(typeof(LogInResponse), 200)]
        public async Task<IActionResult> CheckEmailAvailability(string email)
        {
            var retVal = await _userManagementFactory.CheckEmailAvailability(email);

            return Ok(retVal);
        }

        [HttpPost]
        [Route(nameof(CheckUsernameAvailability))]
        [ProducesResponseType(typeof(LogInResponse), 200)]
        public async Task<IActionResult> CheckUsernameAvailability(UsernameAvailabilityInputModel model)
        {
            var retVal = await _userManagementFactory.CheckUsernameAvailability(model);

            return Ok(retVal);
        }

        [HttpPost]
        [Route("requestPasswordChange")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> RequestPasswordChange(string username)
        {
            if (username == null)
                return BadRequest("An email must be specified.");

            var result = await _userManagementFactory.RequestPasswordChange(username);

            if (result.Success)
                return Ok(result.Message);
            else
                throw new Exception(result.Message);
                 //return BadRequest(result.Message);

        }

        [HttpPost]
        [Route("resendEmailConfirmation")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailConfirmModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userManagementFactory.ResendEmailConfirmation(model.Username);

            if (result.Success)
                return Ok(result.Message);
            else
                throw new Exception(result.Message);
            //return BadRequest(result.Message);
        }


        [HttpPost]
        [Route("resetPassword")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userManagementFactory.ResetPassword(model);

            if (result.Success)
                return Ok(result.Message);
            else
                throw new Exception(result.Message);
            //return BadRequest(result.Message);
        }

        [HttpPost]
        [Authorize]
        [Route("changePassword")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userManagementFactory.ChangePassword(User.FindFirst(ClaimTypes.Name).Value, model);

            if (result.Success)
                return Ok(result);
            else
                throw new Exception(result.Message);
            //return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<OutputModel> ConfirmEmail(string username, string token)
        {
            if (username == null || token == null)
            {
                //return BadRequest("Data missing.");
                throw new Exception("Data missing");
                //return new OutputModel()
                //{
                //    Success = false,
                //    Message = "Data missing."
                //};
            }

            var result = await _userManagementFactory.ConfirmEmail(username, token);

            if (result.Success)
                //return RedirectPermanent("/email-confirmed");
                //return Redirect("/email-confirmed");
                return new OutputModel()
                {
                    Success = true,
                    Message = "Email is successfully confirmed. You can now login to our website."
                };
            else
                // return BadRequest(result.Message);
                throw new Exception("Error happened");
            //return new OutputModel()
            //{
            //    Success = false,
            //    Message = "Error happened"
            //};

        }



    }
}