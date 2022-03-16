using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IProposalService
    {
        /// <summary>
        /// Creates Head Proposal and a proposal's Conversation
        /// </summary>
        /// <param name="ghostwriter"></param>
        /// <param name="project"></param>
        /// <param name="proposalType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>OutputModel</returns>
        Task<ExtendedOutputModel<HeadProposal>> CreateHeadProposal(ApplicationUser ghostwriter, Domain.Entities.Project project, ProposalType proposalType, CancellationToken cancellationToken);

        /// <summary>
        /// Creates Proposal of the proposalType and inserts proposal's ProposalStatusHistory and sets it's status to Active 
        /// </summary>
        /// <param name="headProposal"></param>
        /// <param name="project"></param>
        /// <param name="proposalType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>OutputModel</returns>
        Task<ExtendedOutputModel<Domain.Entities.Proposal>> CreateProposal(HeadProposal headProposal, decimal financialOffer, Domain.Entities.Project project, ProposalType proposalType, CancellationToken cancellationToken);

        /// <summary>
        /// Declines Or Cancels Proposal - sets the proposal into status Declined/Cancelled
        /// </summary>
        /// <param name="initiatorRole"></param>
        /// <param name="proposal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExtendedOutputModel<ProposalStatusHistory>> DeclineCancelProposal(string initiatorRole, Domain.Entities.Proposal proposal, CancellationToken cancellationToken);

        /// <summary>
        /// When searching for projects, exclude projects where there's an active proposal for that author (used in search for live broadcast)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<Domain.Entities.Project> ExcludeActiveProposals(IQueryable<Domain.Entities.Project> query, int authorId);

        /// <summary>
        /// When seatching for fitting authors, exclude the ones that already have an active proposal for the project with projectId (used in assign project to author search)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IQueryable<UserRoleData> ExcludeActiveProposals(IQueryable<UserRoleData> query, int projectId);

        /// <summary>
        /// (used in live broadcast notification, when project gets broadcasted)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<ApplicationUserRole> ExcludeActiveProposals(IQueryable<ApplicationUserRole> query);
    }
}
