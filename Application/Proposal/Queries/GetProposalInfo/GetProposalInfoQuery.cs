using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models.Shared;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Proposal.Queries.GetProposalInfo
{

    public class GetProposalInfoQuery : IRequest<ProposalDetailsDTO>
    {
        public int ProposalId { get; set; }
        public string Username { get; set; }
    }

    public class GetProposalInfoQueryHandler : IRequestHandler<GetProposalInfoQuery, ProposalDetailsDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetProposalInfoQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<ProposalDetailsDTO> Handle(GetProposalInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"Customer {request.Username} not found.");

            if (!_userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName) && !_userManagementFactory.IsInRole(user, UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User {request.Username} is unauthorized to access proposal details information.");

            var retVal = _context.Proposals.Where(x => x.Id == request.ProposalId)
                         .ProjectTo<ProposalDetailsDTO>(_mapper.ConfigurationProvider)
                         .FirstOrDefault();

            if(retVal == null)
                throw new NotFoundException($"Proposal with Id {request.ProposalId} not found.");

            if (retVal.CustomerUsername != request.Username && retVal.AuthorUsername != request.Username)
                throw new AuthorizationException($"User {request.Username} is unauthorized to access proposal details information.");

            return retVal;
        }
    }
}
