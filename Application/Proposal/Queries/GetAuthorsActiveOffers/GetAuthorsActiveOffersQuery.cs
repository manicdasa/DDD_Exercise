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
using System;
using AutoMapper.QueryableExtensions;

namespace GhostWriter.Application.Proposal.Queries.GetAuthorsActiveProposalInfo
{
    public class GetAuthorsActiveOffersQuery : PaginationModel, IRequest<PagedList<ProposalDTO>>
    {
        public string GHWUsername { get; set; }
    }

    public class GetAuthorsActiveOffersQueryHandler : IRequestHandler<GetAuthorsActiveOffersQuery, PagedList<ProposalDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetAuthorsActiveOffersQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProposalDTO>> Handle(GetAuthorsActiveOffersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ghw = await _userManagementFactory.FindUser(request.GHWUsername);

                if (ghw == null)
                    throw new NotFoundException($"Author {request.GHWUsername} not found.");

                if (!_userManagementFactory.IsInRole(ghw, UserRoleDefaults.GhostwriterRoleName))
                    throw new AuthorizationException($"User {request.GHWUsername} is unauthorized to access active projects tab.");

                var query = _context.Proposals.Where(x =>
                            x.HeadProposal.GHWId == ghw.Id
                            && x.ChildProposal == null
                            && x.ProposalType == ProposalType.Offer
                            && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active)
                    .OrderByDescending(x => x.LastUpdate)
                    .ProjectTo<ProposalDTO>(_mapper.ConfigurationProvider);

                if (request.Page != default || request.PageSize != default)
                    return new PagedList<ProposalDTO>(query, request.Page, request.PageSize);
                else
                    return new PagedList<ProposalDTO>(query);
            }
            catch(Exception ex)
            {
                //return null
                throw ex;
            }
        }
    }
}
