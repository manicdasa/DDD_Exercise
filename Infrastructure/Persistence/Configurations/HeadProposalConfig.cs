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
    public class HeadProposalConfig : IEntityTypeConfiguration<HeadProposal>
    {
        public void Configure(EntityTypeBuilder<HeadProposal> builder)
        {
            builder.ToTable(nameof(HeadProposal));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Ghostwriter)
              .WithMany(x => x.Proposals)
              .HasForeignKey(ur => ur.GHWId).IsRequired();
        }
    }
}
