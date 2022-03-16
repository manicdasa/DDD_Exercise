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
using System;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Application.Proposal.Queries.GetCustomerBids
{
    public class GetCustomerBidsQuery : PaginationModel
    {
        public List<int> KindOfWorkIds { get; set; }
        public List<int> AreaOfExpertiseIds { get; set; }
        public List<int> LanguageIds { get; set; }
        public int? MinimumDegreeId { get; set; }
        public int? NoPagesFromRange { get; set; }
        public int? NoPagesToRange { get; set; }
        public DateTime Deadline { get; set; }
    }

    public class GetCustomerBidsQueryExtended : GetCustomerBidsQuery, IRequest<PagedList<ProjectBidsDTO>>
    {
        public string CustomerUsername { get; set; }
    }

    public class GetCustomerBidsQueryHandler : IRequestHandler<GetCustomerBidsQueryExtended, PagedList<ProjectBidsDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetCustomerBidsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<PagedList<ProjectBidsDTO>> Handle(GetCustomerBidsQueryExtended request, CancellationToken cancellationToken)
        {
            var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

            if (customer == null)
                throw new NotFoundException($"Customer {request.CustomerUsername} not found.");

            if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User {request.CustomerUsername} is unauthorized to access active projects tab.");

            try
            {
                Degree minimumDegree = request.MinimumDegreeId is null ? null : _context.Degrees.Find(request.MinimumDegreeId);

                var query = _context.Projects.Where(x => x.CustomerId == customer.Id 
                                             && (x.ProjectStatus == ProjectStatus.Open || x.ProjectStatus == ProjectStatus.InCreation)
                                             && (request.KindOfWorkIds == null || !request.KindOfWorkIds.Any() || request.KindOfWorkIds.Contains(x.KindOfWork.Id))
                                             && (request.LanguageIds == null || !request.LanguageIds.Any() || request.LanguageIds.Contains(x.Language.Id))
                                             && (request.MinimumDegreeId == null || x.MinimumDegree.Stage <= minimumDegree.Stage)
                                             && (request.NoPagesFromRange == null || (request.NoPagesFromRange <= x.PagesNo))
                                             && (request.NoPagesToRange == null || (request.NoPagesToRange >= x.PagesNo))
                                             && (request.Deadline == default(DateTime) || (DateTime)request.Deadline >= x.Deadline));

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
                    .ProjectTo<ProjectBidsDTO>(_mapper.ConfigurationProvider);

                PagedList<ProjectBidsDTO> retVal;
                if (request.Page != default || request.PageSize != default)
                    retVal = new PagedList<ProjectBidsDTO>(proposals, request.Page, request.PageSize);
                else
                    retVal = new PagedList<ProjectBidsDTO>(proposals);

                foreach (var project in retVal.Items)
                {
                    var bids = _context.Proposals.Where(x => x.HeadProposal.Project.Id == project.ProjectId && x.ProposalType == ProposalType.Bid && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active);
                    decimal maxOffer = bids.Any() ? bids.Max(x => x.FinancialOffer) : 0;

                    project.BidsNo = bids.Count();
                    project.HighestBid = bids.Where(x => x.FinancialOffer == maxOffer).ProjectTo<ProposalDetails1DTO>(_mapper.ConfigurationProvider).FirstOrDefault();
                }

                return retVal;
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
           
        }
    }
}
