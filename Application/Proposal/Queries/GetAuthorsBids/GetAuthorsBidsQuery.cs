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

namespace GhostWriter.Application.Proposal.Queries.GetAuthorsActiveProposalInfo
{
    public class GetAuthorsBidsQuery : PaginationModel, IRequest<PagedList<ProposalDTO>>
    {
        public string GHWUsername { get; set; }
    }

    public class GetAuthorsBidsQueryHandler : IRequestHandler<GetAuthorsBidsQuery, PagedList<ProposalDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetAuthorsBidsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProposalDTO>> Handle(GetAuthorsBidsQuery request, CancellationToken cancellationToken)
        {
            var ghw = await _userManagementFactory.FindUser(request.GHWUsername);

            if (ghw == null)
                throw new NotFoundException($"Author {request.GHWUsername} not found.");

            if (!_userManagementFactory.IsInRole(ghw, UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User {request.GHWUsername} is unauthorized to access active projects tab.");

            var query = _context.Proposals.Where(x =>
                        x.HeadProposal.Ghostwriter.Id == ghw.Id
                        && x.ProposalType == ProposalType.Bid
                        && x.ChildProposal == null
                        && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active)
                .OrderByDescending(x => x.LastUpdate)
                .Select(x => new ProposalDTO()
                {
                    CustomerUsername = x.HeadProposal.Project.Customer.UserName,
                    Deadline = x.HeadProposal.Project.Deadline,
                    Id = x.Id,
                    ProjectId = x.HeadProposal.Project.Id,
                    KindOfWorkDTO = new KindOfWorkDTO() { Id = x.HeadProposal.Project.KindOfWork.Id, Value = x.HeadProposal.Project.KindOfWork.Value, Description = x.HeadProposal.Project.KindOfWork.Description },
                    LanguageDTO = new LanguageDTO() { Id = x.HeadProposal.Project.Language.Id, Value = x.HeadProposal.Project.Language.Value },
                    FinancialOffer = x.HeadProposal.Project.MaxBudget,
                    PagesNo = x.HeadProposal.Project.PagesNo,
                    ProjectTopic = x.HeadProposal.Project.ProjectTopic,
                    HeadProposalId = x.HeadProposal.Id,
                    //ProposalStatus = x.ProposalStatuses.Any() ? x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus.ToString() : string.Empty
                });

            if (request.Page != default || request.PageSize != default)
                return new PagedList<ProposalDTO>(query, request.Page, request.PageSize);
            else
                return new PagedList<ProposalDTO>(query);
        }
    }
}
