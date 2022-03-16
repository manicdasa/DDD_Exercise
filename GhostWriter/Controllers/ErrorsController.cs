using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GhostWriter.WebUI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var code = 500; // Internal Server Error by default

            if (exception is AuthorizationException) code = 401; // Not Found
            else if (exception is NotFoundException) code = 404; // Unauthorized
            else if (exception is Exception) code = 400; // Bad Request
            SentrySdk.ConfigureScope(scope =>
            {
                scope.User = new User
                {
                    //Email = user.Email,
                    Username = User.FindFirst(ClaimTypes.Name).Value
                };
            });
            SentrySdk.CaptureMessage(context.Error.Message);
            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new ErrorResponse(exception); // Your error model
        }
    }
}
