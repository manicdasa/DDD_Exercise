using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Infrastructure.Configurations
{
    public class BuzzwordConfig : IEntityTypeConfiguration<Buzzword>
    {
        public void Configure(EntityTypeBuilder<Buzzword> builder)
        {
            builder.ToTable(nameof(Buzzword));
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
