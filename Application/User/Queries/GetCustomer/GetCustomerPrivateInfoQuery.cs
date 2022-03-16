using AutoMapper;
using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace GhostWriter.Application.User.Queries
{
    public class GetCustomerPrivateInfoQuery : IRequest<CustomerPrivateInfoDTO>
    {
        public int CustomerId { get; set; }
    }

    public class GetCustomerPrivateInfoQueryHandler : IRequestHandler<GetCustomerPrivateInfoQuery, CustomerPrivateInfoDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetCustomerPrivateInfoQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<CustomerPrivateInfoDTO> Handle(GetCustomerPrivateInfoQuery request, CancellationToken cancellationToken)
        {
            var customer = _userManagementFactory.FindUserById(request.CustomerId);

            if (customer == null)
                throw new NotFoundException($"Usernot found.");

            if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User {customer.UserName} is unauthorized to access customer's private information.");

            try
            {
                var customerRole = _userManagementFactory.FindRoleIdByName(UserRoleDefaults.CustomerRoleName);

                var additionalData = _context.UserRoleDatas.Where(x => x.ApplicationUserRole.UserId == customer.Id && x.ApplicationUserRole.RoleId == customerRole)
                    .ProjectTo<CustomerPrivateInfoDTO>(_mapper.ConfigurationProvider).FirstOrDefault();

                if (additionalData == null)
                    throw new NotFoundException($"Additional information about the customer {customer.UserName} was not found.");

                additionalData.PagesWrittenSoFar = _context.Bookings.Where(x =>
                        x.HeadProposal.Project.CustomerId == request.CustomerId
                        && BookingStatusGroups.Closed.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus))
                    .Select(x => x.HeadProposal.Project)
                    .Sum(x => x.PagesNo);

                additionalData.PagesNoInProgress = _context.Bookings.Where(x =>
                      x.HeadProposal.Project.CustomerId == request.CustomerId
                      && BookingStatusGroups.Open.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus))
                  .Select(x => x.HeadProposal.Project)
                  .Sum(x => x.PagesNo);

                additionalData.NoActiveBids = _context.Proposals.Where(x =>
                        x.HeadProposal.Project.CustomerId == request.CustomerId
                        && x.ChildProposal == null
                        && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == Domain.Enums.ProposalStatus.Active
                        && x.ProposalType == Domain.Enums.ProposalType.Bid)
                        .Count();

                return additionalData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
