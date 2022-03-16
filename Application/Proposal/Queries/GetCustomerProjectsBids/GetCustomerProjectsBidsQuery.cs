using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.Common.Models.Shared;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Proposal.Queries.GetCustomerProjectsBids
{
    public class GetCustomerProjectsBidsQuery : PaginationModel, IRequest<PagedList<ProposalDTO>>
    {
        public string CustomerUsername { get; set; }
        public int ProjectId { get; set; }
    }

    public class GetCustomerProjectsBidsQueryHandler : IRequestHandler<GetCustomerProjectsBidsQuery, PagedList<ProposalDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetCustomerProjectsBidsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProposalDTO>> Handle(GetCustomerProjectsBidsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

            if (customer == null)
                throw new NotFoundException($"Customer {request.CustomerUsername} not found.");

            if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User {request.CustomerUsername} is unauthorized to access active projects tab.");

            var query = _context.Proposals.Where(x =>
                        x.HeadProposal.Project.CustomerId == customer.Id
                        && x.HeadProposal.Project.Id == request.ProjectId
                        && x.ProposalType == ProposalType.Bid
                        && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active)
                .OrderByDescending(x => x.LastUpdate)
                .ProjectTo<ProposalDTO>(_mapper.ConfigurationProvider);

            PagedList<ProposalDTO> retVal;

            if (request.Page != default || request.PageSize != default)
                retVal = new PagedList<ProposalDTO>(query, request.Page, request.PageSize);
            else
                retVal = new PagedList<ProposalDTO>(query);

            return retVal;
        }
    }
}
