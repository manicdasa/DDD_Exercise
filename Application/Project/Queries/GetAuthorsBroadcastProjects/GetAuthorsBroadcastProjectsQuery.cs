using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Enums;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Models.Shared;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.Project.Queries.GetAuthorsBroadcastProjects
{
    public class GetAuthorsBroadcastProjectsQuery : PaginationModel
    {
        public List<int> KindOfWorkIds { get; set; }
        public List<int> AreaOfExpertiseIds { get; set; }
        public List<int> LanguageIds { get; set; }
        public int? MinimumDegreeId { get; set; }
        public int? NoPagesFromRange { get; set; }
        public int? NoPagesToRange { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class GetAuthorsBroadcastProjectsQueryExtended : GetAuthorsBroadcastProjectsQuery, IRequest<PagedList<ProjectDTO>>, IMapFrom<GetAuthorsBroadcastProjectsQuery>
    {
        public string GHWUsername { get; set; }
        public int GHWId { get; set; }
    }

    public class GetAuthorsBroadcastProjectsQueryHandler : IRequestHandler<GetAuthorsBroadcastProjectsQueryExtended, PagedList<ProjectDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProposalService _proposalService;

        public GetAuthorsBroadcastProjectsQueryHandler(IApplicationDbContext context, IMapper mapper, IProposalService proposalService)
        {
            _context = context;
            _mapper = mapper;
            _proposalService = proposalService;
        }

        public async Task<PagedList<ProjectDTO>> Handle(GetAuthorsBroadcastProjectsQueryExtended request, CancellationToken cancellationToken)
        {
            try
            {
                Degree minimumDegree = request.MinimumDegreeId is null ? null :_context.Degrees.Find(request.MinimumDegreeId);

                var query = _context.Projects.Where(x =>
                    (x.ProjectStatus == ProjectStatus.Open || x.ProjectStatus == ProjectStatus.InCreation)
                    && x.IsPublished
                    && (!request.LanguageIds.Any() || request.LanguageIds.Contains(x.Language.Id))
                    && (!request.KindOfWorkIds.Any() || request.KindOfWorkIds.Contains(x.KindOfWork.Id))
                    && (request.MinimumDegreeId == null || x.MinimumDegree.Stage <= minimumDegree.Stage)
                    && (request.NoPagesFromRange == null || request.NoPagesFromRange <= x.PagesNo)
                    && (request.NoPagesToRange == null || x.PagesNo <= request.NoPagesToRange)
                    && (request.Deadline == null || (DateTime)request.Deadline >= x.Deadline));

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

                query = _proposalService.ExcludeActiveProposals(query, request.GHWId);

                var projects = query
                    .OrderByDescending(x => x.LastUpdate)
                    .ProjectTo<ProjectDTO>(_mapper.ConfigurationProvider);

                PagedList<ProjectDTO> retVal;

                if (request.Page != default || request.PageSize != default)
                    retVal = new PagedList<ProjectDTO>(projects, request.Page, request.PageSize);
                else
                    retVal = new PagedList<ProjectDTO>(projects);

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }
    }
}
