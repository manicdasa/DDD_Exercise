using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Infrastructure.Configurations
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable(nameof(Booking));
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Documents)
            .WithOne(x => x.Booking)
            .HasForeignKey(ur => ur.BookingId).IsRequired(false);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
