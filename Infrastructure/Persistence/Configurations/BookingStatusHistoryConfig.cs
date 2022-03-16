using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Infrastructure.Configurations
{
    public class BookingStatusHistoryConfig : IEntityTypeConfiguration<BookingStatusHistory>
    {
        public void Configure(EntityTypeBuilder<BookingStatusHistory> builder)
        {
            builder.ToTable(nameof(BookingStatusHistory));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
