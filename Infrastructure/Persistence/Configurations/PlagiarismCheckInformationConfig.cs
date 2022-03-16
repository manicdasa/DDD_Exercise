using GhostWriter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Infrastructure.Configurations
{
    class PlagiarismCheckInformationConfig : IEntityTypeConfiguration<PlagiarismCheckInformation>
    {
        public void Configure(EntityTypeBuilder<PlagiarismCheckInformation> builder)
        {
            builder.ToTable(nameof(PlagiarismCheckInformation));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Document)
                .WithMany(x => x.PlagiarismCheckInformation)
                .HasForeignKey(ur => ur.DocumentId).IsRequired();
        }
    }

}
