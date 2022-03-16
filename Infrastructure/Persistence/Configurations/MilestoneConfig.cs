using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Infrastructure.Configurations
{
    public class MilestoneConfig : IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {
            builder.ToTable(nameof(Milestone));
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
