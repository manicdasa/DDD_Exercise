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

namespace GhostWriter.Application.Proposal.Queries.GetCustomerGeneratedOffers
{
    public class GetCustomerProposalsQuery : PaginationModel, IRequest<PagedList<ProposalDTO>>
    {
        public string CustomerUsername { get; set; }
        public List<ProposalStatus> PossibleStatuses { get; set; }
        public ProposalType ProposalType { get; set; }
    }

    public class GetCustomerProposalsQueryHandler : IRequestHandler<GetCustomerProposalsQuery, PagedList<ProposalDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetCustomerProposalsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProposalDTO>> Handle(GetCustomerProposalsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

            if (customer == null)
                throw new NotFoundException($"Customer {request.CustomerUsername} not found.");

            if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User {request.CustomerUsername} is unauthorized to access active projects tab.");

            var query = _context.Proposals.Where(x =>
                        x.HeadProposal.Project.CustomerId == customer.Id
                        && x.ProposalType == request.ProposalType
                        && x.ChildProposal == null
                        && request.PossibleStatuses.Contains(x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus))
                .OrderByDescending(x => x.LastUpdate)
                .ProjectTo<ProposalDTO>(_mapper.ConfigurationProvider);

            if (request.Page != default || request.PageSize != default)
                return new PagedList<ProposalDTO>(query, request.Page, request.PageSize);
            else
                return new PagedList<ProposalDTO>(query);
        }
    }
}
