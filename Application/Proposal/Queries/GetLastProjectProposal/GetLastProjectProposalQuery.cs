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

namespace GhostWriter.Application.Proposal.Queries.GetLastProjectProposal
{
    public class GetLastProjectProposalQuery : IRequest<ProposalInfoDTO>
    {
        public string Username { get; set; }
        public int ProjectId { get; set; }
        public ProposalType ProposalType { get; set; }
        public string RoleName { get; set; }
        public bool ShowOnlyActiveProposals { get; set; }
    }

    public class GetLastProjectProposalQueryHandler : IRequestHandler<GetLastProjectProposalQuery, ProposalInfoDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetLastProjectProposalQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<ProposalInfoDTO> Handle(GetLastProjectProposalQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"Customer {request.Username} not found.");

            var proposal = _context.Proposals.Where(x =>
                        request.RoleName == UserRoleDefaults.CustomerRoleName ? x.HeadProposal.Project.CustomerId == user.Id : x.HeadProposal.Ghostwriter.Id == user.Id
                        && x.HeadProposal.Project.Id == request.ProjectId
                        && x.ProposalType == request.ProposalType
                        && (request.ShowOnlyActiveProposals ? x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active : 1==1))
                         .ProjectTo<ProposalInfoDTO>(_mapper.ConfigurationProvider).FirstOrDefault();

            return proposal;
        }
    }
}
