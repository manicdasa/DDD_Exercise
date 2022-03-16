using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.DTOs;
using AutoMapper;
using GhostWriter.Application.AdminDashboard.Queries;
using GhostWriter.Application.Common.Models.Shared;
using System.Collections.Generic;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.AdminDashboard.Commands;
using GhostWriter.Domain.Defaults;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiControllerBase
    {
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Pay to author
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(nameof(PayAuthor))]
        public async Task<ActionResult<OutputModel>> PayAuthor(int bookingId, decimal paymentAmount)
        {
            PayAuthorCommand request = new PayAuthorCommand()
            {
                BookingId = bookingId,
                AdminUsername = User.FindFirst(ClaimTypes.Name).Value,
                PaymentAmount = paymentAmount
            };

            return await Mediator.Send(request);
        }

        [HttpPost(nameof(MarkAsPaidAuthor))]
        public async Task<ActionResult<OutputModel>> MarkAsPaidAuthor(int bookingId, decimal paymentAmount)
        {
            MarkAsPaidAuthorCommand request = new MarkAsPaidAuthorCommand()
            {
                BookingId = bookingId,
                AdminUsername = User.FindFirst(ClaimTypes.Name).Value,
                PaymentAmount = paymentAmount
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets Projects Datatable
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetProjectsDatatable))]
        public async Task<PagedList<BookingShortInfoDTO>> GetProjectsDatatable([FromQuery] LookupInputModel dtRequest)
        {
            GetBookingsDatatableQuery request = new GetBookingsDatatableQuery()
            {
                Search = dtRequest.Search,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets Dashboard Statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetDashboardStats))]
        public async Task<ActionResult<DashboardStatsDTO>> GetDashboardStats()
        {
            GetDashboardStatsQuery request = new GetDashboardStatsQuery()
            {
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets Author statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorStats))]
        public async Task<ActionResult<PagedList<AuthorStatsDTO>>> GetAuthorStats([FromQuery] LookupInputModel dtRequest)
        {
            GetAuthorsStatsQuery request = new GetAuthorsStatsQuery()
            {
                Username = User.FindFirst(ClaimTypes.Name).Value,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets Customers statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomerStats))]
        public async Task<ActionResult<PagedList<CustomerStatsDTO>>> GetCustomerStats([FromQuery] LookupInputModel dtRequest)
        {
            GetCustomersStatsQuery request = new GetCustomersStatsQuery()
            {
                Username = User.FindFirst(ClaimTypes.Name).Value,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Statistics tab - New Projects (projects where the payment is not done yet)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetClosedUnpaidProjects))]
        public async Task<PagedList<BookingPaymentAdminDTO>> GetClosedUnpaidProjects([FromQuery] LookupInputModel dtRequest)
        {
            GetClosedUnpaidProjectsQuery request = new GetClosedUnpaidProjectsQuery()
            {
                BookingStatuses = BookingStatusGroups.ClosedNoCancelled,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Statistics tab - New Projects (projects where the payment is not done yet)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetNewProjects))]
        public async Task<PagedList<BookingAdminDTO>> GetNewProjects([FromQuery] LookupInputModel dtRequest)
        {
            GetBookingsByStatusQuery request = new GetBookingsByStatusQuery()
            {
                BookingStatuses = BookingStatusGroups.Inactive,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Statistics tab - Active Projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetActiveProjects))]
        public async Task<PagedList<BookingAdminDTO>> GetActiveProjects([FromQuery] LookupInputModel dtRequest)
        {
            GetBookingsByStatusQuery request = new GetBookingsByStatusQuery()
            {
                BookingStatuses = BookingStatusGroups.ActiveNoDispute,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
                OrderColumn = dtRequest.OrderColumn,

                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Statistics tab - Projects in Dispute
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetProjectsInDispute))]
        public async Task<PagedList<BookingAdminDTO>> GetProjectsInDispute([FromQuery] LookupInputModel dtRequest)
        {
            GetBookingsByStatusQuery request = new GetBookingsByStatusQuery()
            {
                BookingStatuses = BookingStatusGroups.InDispute,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Statistics tab - Completed (Archived) Projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetArchivedProjects))]
        public async Task<PagedList<BookingAdminDTO>> GetArchivedProjects([FromQuery] LookupInputModel dtRequest)
        {
            GetBookingsByStatusQuery request = new GetBookingsByStatusQuery()
            {
                BookingStatuses = BookingStatusGroups.Closed,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets statistics about new customers (registered to the portal in the last 30 days)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetNewUsers))]
        public async Task<PagedList<CustomerDetailedStatsDTO>> GetNewUsers([FromQuery] LookupInputModel dtRequest)
        {
            GetCustomersDetailedStatsQuery request = new GetCustomersDetailedStatsQuery()
            {
                UserType = UserType.NewUser,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Search = dtRequest.Search,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,

            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets statistics about the active customers (registered to the portal in the last 30 days/have booking/project made in last 30 days)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetActiveUsers))]
        public async Task<PagedList<CustomerDetailedStatsDTO>> GetActiveUsers([FromQuery] LookupInputModel dtRequest)
        {
            GetCustomersDetailedStatsQuery request = new GetCustomersDetailedStatsQuery()
            {
                UserType = UserType.ActiveUser,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                OrderColumn = dtRequest.OrderColumn,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                Search = dtRequest.Search,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets statistics about the inactive customers (didn't registered to the portal in the last 30 days/have booking/project made in last 30 days)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetInactiveUsers))]
        public async Task<PagedList<CustomerDetailedStatsDTO>> GetInactiveUsers([FromQuery] LookupInputModel dtRequest)
        {
            GetCustomersDetailedStatsQuery request = new GetCustomersDetailedStatsQuery()
            {
                UserType = UserType.InactiveUser,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
                Search = dtRequest.Search
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets statistics about new authors (registered to the portal in the last 30 days)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetNewAuthors))]
        public async Task<PagedList<AuthorDetailedStatsDTO>> GetNewAuthors([FromQuery] LookupInputModel dtRequest)
        {
            GetAuthorsDetailedStatsQuery request = new GetAuthorsDetailedStatsQuery()
            {
                UserType = UserType.NewUser,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
                Search = dtRequest.Search,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets statistics about the active authors (registered to the portal in the last 30 days/have booking/project made in last 30 days)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetActiveAuthors))]
        public async Task<PagedList<AuthorDetailedStatsDTO>> GetActiveAuthors([FromQuery] LookupInputModel dtRequest)
        {
            GetAuthorsDetailedStatsQuery request = new GetAuthorsDetailedStatsQuery()
            {
                UserType = UserType.ActiveUser,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
                Search=dtRequest.Search,
            };

            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets statistics about the inactive authors (didn't registered to the portal in the last 30 days/have booking/project made in last 30 days)
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetInactiveAuthors))]
        public async Task<PagedList<AuthorDetailedStatsDTO>> GetInactiveAuthors([FromQuery] LookupInputModel dtRequest)
        {
            GetAuthorsDetailedStatsQuery request = new GetAuthorsDetailedStatsQuery()
            {
                UserType = UserType.InactiveUser,
                Page = dtRequest.Page,
                PageSize = dtRequest.PageSize,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                OrderColumn = dtRequest.OrderColumn,
                Search = dtRequest.Search,
            };

            return await Mediator.Send(request);
        }
    }
}