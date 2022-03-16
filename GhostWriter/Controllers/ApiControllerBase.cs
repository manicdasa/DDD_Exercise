using GhostWriter.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private IUserManagementFactory _userManagementFactory;

        private ISender _mediator;

        protected IUserManagementFactory UserManagementFactory => _userManagementFactory ??= HttpContext.RequestServices.GetService<IUserManagementFactory>();
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();


    }
}