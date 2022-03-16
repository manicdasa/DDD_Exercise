using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Document> Documents { get; set; }
        DbSet<Domain.Entities.Booking> Bookings { get; set; }
        DbSet<Buzzword> Buzzwords { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<Degree> Degrees { get; set; }
        DbSet<Dispute> Disputes { get; set; }
        DbSet<ExpertiseArea> ExpertiseAreas { get; set; }
        DbSet<HeadProposal> HeadProposals { get; set; }
        DbSet<KindOfWork> KindOfWorks { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Milestone> Milestones { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Picture> Pictures { get; set; }
        DbSet<Domain.Entities.Project> Projects { get; set; }
        DbSet<Domain.Entities.Proposal> Proposals { get; set; }
        DbSet<ProposalStatusHistory> ProposalStatusHistories { get; set; }
        DbSet<BookingStatusHistory> BookingStatusHistories { get; set; }
        DbSet<Rate> Rates { get; set; }
        DbSet<ServiceCharge> ServiceCharges { get; set; }
        DbSet<ServiceChargeType> ServiceChargeTypes { get; set; }
        DbSet<UserRoleData> UserRoleDatas { get; set; }
        DbSet<ApplicationRole> ApplicationRoles { get; set; }
        DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<PlagiarismCheckInformation> PlagiarismCheckInformations { get; set; }
        DbSet<Domain.Entities.Notification> Notifications { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
