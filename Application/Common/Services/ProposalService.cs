using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using GhostWriter.Domain.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IApplicationDbContext _context;
        private readonly IPriceCalculatorService _priceCalculatorService;

        public ProposalService(IApplicationDbContext context, IPriceCalculatorService priceCalculatorService)
        {
            _context = context;
            _priceCalculatorService = priceCalculatorService;
        }
        public async Task<ExtendedOutputModel<Domain.Entities.Proposal>> CreateProposal(HeadProposal headProposal, decimal financialOffer, Domain.Entities.Project project, ProposalType proposalType, CancellationToken cancellationToken)
        {
            try
            {
                var lastProposal = _context.Proposals.Where(x => x.HeadProposal == headProposal && x.ChildProposal == null).FirstOrDefault();


                Domain.Entities.Proposal proposal = new Domain.Entities.Proposal()
                {
                    HeadProposal = headProposal,
                    DateCreated = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow,
                    Deadline = project.Deadline,
                    FinancialOffer = proposalType == ProposalType.Bid ? financialOffer : project.MaxBudget,
                    ServiceCharges = proposalType == ProposalType.Bid ? _priceCalculatorService.CalculateServiceCharges(financialOffer, project.PagesNo) : project.CalculatedServiceCharges,
                    ProposalType = proposalType
                };

                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync(cancellationToken);

                if (lastProposal != null)
                {
                    lastProposal.ChildProposalId = proposal.Id;
                    proposal.ParentProposalId = lastProposal.Id;

                    _context.Proposals.UpdateRange(proposal, lastProposal);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                ProposalStatusHistory proposalStatusHistory = new ProposalStatusHistory()
                {
                    DateCreated = DateTime.UtcNow,
                    Proposal = proposal,
                    ProposalStatus = ProposalStatus.Active
                };

                _context.ProposalStatusHistories.Add(proposalStatusHistory);
                await _context.SaveChangesAsync(cancellationToken);

                return new ExtendedOutputModel<Domain.Entities.Proposal>()
                {
                    Success = true,
                    Message = string.Empty,
                    AdditionalInformation = proposal
                };
            }
            catch (Exception ex)
            {
                return new ExtendedOutputModel<Domain.Entities.Proposal>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ExtendedOutputModel<HeadProposal>> CreateHeadProposal(ApplicationUser ghostwriter, Domain.Entities.Project project, ProposalType proposalType, CancellationToken cancellationToken)
        {
            try
            {
                var headProposal = new HeadProposal()
                {
                    Ghostwriter = ghostwriter,
                    Project = project,
                    ProposalType = proposalType,
                    LastUpdate = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow
                };

                _context.HeadProposals.Add(headProposal);
                await _context.SaveChangesAsync(cancellationToken);

                _context.Projects.Update(project);

                var conversation = new Conversation()
                {
                    DateCreated = DateTime.UtcNow,
                    HeadProposal = headProposal
                };

                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync(cancellationToken);

                return new ExtendedOutputModel<HeadProposal>()
                {
                    AdditionalInformation = headProposal,
                    Success = true,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ExtendedOutputModel<HeadProposal>()
                {
                    AdditionalInformation = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ExtendedOutputModel<ProposalStatusHistory>> DeclineCancelProposal(string initiatorRole, Domain.Entities.Proposal proposal, CancellationToken cancellationToken)
        {
            var lastStatus = _context.ProposalStatusHistories.Where(x => x.Proposal == proposal).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            ProposalStatusHistory newStatus;
            switch (lastStatus.ProposalStatus)
            {
                case ProposalStatus.Accepted:
                    throw new Exception($"The offer is already accepted and the payment by the user has been made.");

                case ProposalStatus.Cancelled:
                    throw new Exception($"The offer is already cancelled.");

                case ProposalStatus.Deleted:
                    throw new Exception($"The offer is deleted.");

                case ProposalStatus.Declined:
                    throw new Exception($"The offer is already declined.");

                case ProposalStatus.Active:
                    var statusToSet = ProposalStatus.Declined;
                    if ((initiatorRole == UserRoleDefaults.CustomerRoleName && proposal.ProposalType == ProposalType.Offer)
                        || (initiatorRole == UserRoleDefaults.GhostwriterRoleName && proposal.ProposalType == ProposalType.Bid))
                        statusToSet = ProposalStatus.Cancelled;

                    newStatus = new ProposalStatusHistory()
                    {
                        DateCreated = DateTime.UtcNow,
                        Proposal = proposal,
                        ProposalStatus = statusToSet
                    };
                    proposal.LastUpdate = DateTime.UtcNow;
                    _context.ProposalStatusHistories.Add(newStatus);
                    _context.Proposals.Update(proposal);
                    await _context.SaveChangesAsync(cancellationToken);

                    break;
                default:
                    throw new Exception($"Proposal status not found."); ;
            }

            return new ExtendedOutputModel<ProposalStatusHistory>()
            {
                Success = true,
                Message = string.Empty,
                AdditionalInformation = newStatus
            };
        }

        public IQueryable<Domain.Entities.Project> ExcludeActiveProposals(IQueryable<Domain.Entities.Project> query, int authorId)
        {
            return query.Where(x => !_context.Proposals.Where(y => y.HeadProposal.Project.Id == x.Id && y.HeadProposal.GHWId == authorId && y.ChildProposal == null && y.ProposalStatuses.OrderByDescending(z => z.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active).Any());
        }

        public IQueryable<UserRoleData> ExcludeActiveProposals(IQueryable<UserRoleData> query, int projectId)
        {
            return query.Where(x => !_context.Proposals.Where(y => y.HeadProposal.Project.Id == projectId && y.HeadProposal.GHWId == x.ApplicationUserRole.UserId && y.ChildProposal == null && y.ProposalStatuses.OrderByDescending(z => z.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active).Any());
        }

        public IQueryable<ApplicationUserRole> ExcludeActiveProposals(IQueryable<ApplicationUserRole> query)
        {
            return query.Where(x => !_context.Proposals.Where(y => y.HeadProposal.GHWId == x.UserId && y.ChildProposal == null && y.ProposalStatuses.OrderByDescending(z => z.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active).Any());
        }
    }
}
