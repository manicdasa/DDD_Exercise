using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Entities;
using AutoMapper.QueryableExtensions;

namespace GhostWriter.Application.Proposal.Queries.GetAuthorsActiveProposalInfo
{
    public class GetAuthorsActiveProposalsInfoQuery : PaginationModel, IRequest<PagedList<ProjectShortInfoDTO>>
    {
        public string GHWUsername { get; set; }
        public List<int> KindOfWorkIds { get; set; }
        public List<int> AreaOfExpertiseIds { get; set; }
        public List<int> LanguageIds { get; set; }
        public int? MinimumDegreeId { get; set; }
        public int? NoPagesFromRange { get; set; }
        public int? NoPagesToRange { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class GetAuthorsActiveProposalsInfoQueryHandler : IRequestHandler<GetAuthorsActiveProposalsInfoQuery, PagedList<ProjectShortInfoDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetAuthorsActiveProposalsInfoQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProjectShortInfoDTO>> Handle(GetAuthorsActiveProposalsInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ghw = await _userManagementFactory.FindUser(request.GHWUsername);

                if (ghw == null)
                    throw new NotFoundException($"Author {request.GHWUsername} not found.");

                if (!_userManagementFactory.IsInRole(ghw, UserRoleDefaults.GhostwriterRoleName))
                    throw new AuthorizationException($"User {request.GHWUsername} is unauthorized to active projects tab.");

                Degree minimumDegree = request.MinimumDegreeId is null ? null : _context.Degrees.Find(request.MinimumDegreeId);

                var query = _context.Proposals.Where(x =>
                            x.HeadProposal.GHWId == ghw.Id
                            && x.ChildProposal == null
                            && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active)
                            .Select(x => x.HeadProposal.Project)
                            .Where(y => (request.KindOfWorkIds == null || request.KindOfWorkIds.Count == 0 || request.KindOfWorkIds.Contains(y.KindOfWorkId))
                                    && (!request.LanguageIds.Any() || request.LanguageIds.Contains(y.Language.Id))
                                    && (request.MinimumDegreeId == null || y.MinimumDegree.Stage <= minimumDegree.Stage)
                                    && (request.NoPagesFromRange == null || request.NoPagesFromRange <= y.PagesNo)
                                    && (request.NoPagesToRange == null || y.PagesNo <= request.NoPagesToRange)
                                    && (request.Deadline == null || (DateTime)request.Deadline <= y.Deadline));

                if (request.AreaOfExpertiseIds != null && request.AreaOfExpertiseIds.Any())
                {
                    List<int> projectIds = new List<int>();
                    foreach (var exparId in request.AreaOfExpertiseIds ?? new List<int>())
                    {
                        var expertiseArea = _context.ExpertiseAreas.Find(exparId);

                        if (expertiseArea != null)
                        {
                            projectIds.AddRange(query.Where(x => x.ExpertiseAreas.Contains(expertiseArea)).Select(x => x.Id));
                        }
                    }
                    query = query.Where(x => projectIds.Contains(x.Id));
                }

                var proposals = query
                    .OrderByDescending(x => x.LastUpdate)
                    .ProjectTo<ProjectShortInfoDTO>(_mapper.ConfigurationProvider);

                if (request.Page != default || request.PageSize != default)
                    return new PagedList<ProjectShortInfoDTO>(proposals, request.Page, request.PageSize);
                else
                    return new PagedList<ProjectShortInfoDTO>(proposals);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
