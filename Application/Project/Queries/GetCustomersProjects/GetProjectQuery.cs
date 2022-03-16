using AutoMapper;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Project.Queries
{
    public class GetProjectQuery : IRequest<ProjectDetailsDTO>
    {
        public int ProjectId { get; set; }
        public string Username { get; set; }
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDetailsDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetProjectQueryHandler(IApplicationDbContext context, IMapper mapper, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<ProjectDetailsDTO> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = _context.Projects.Where(x => x.Id == request.ProjectId)
                .ProjectTo<ProjectDetailsDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (project is null)
                return project;

            project.CustomerPublicInfoDTO = _userManagementFactory.GetUsersAdditionalData(project.CustomerUsername, UserRoleDefaults.CustomerRoleName)
            .ProjectTo<CustomerPublicInfoDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

            List<ProposalStatus> statuses = new List<ProposalStatus>() { ProposalStatus.Active };

            project.ProposalDetailsDTOs = _context.Proposals
                .Where(x => x.HeadProposal.Project.Id == request.ProjectId 
                        && x.ChildProposal == null
                        && statuses.Contains(x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus)
                        && (x.HeadProposal.Ghostwriter.UserName == request.Username || x.HeadProposal.Project.Customer.UserName == request.Username))
                 .ProjectTo<ProposalInfoDTO>(_mapper.ConfigurationProvider)
                 .ToList();

            return project;
        }
    }
}
