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
    public class ProposalStatusHistoryConfig : IEntityTypeConfiguration<ProposalStatusHistory>
    {
        public void Configure(EntityTypeBuilder<ProposalStatusHistory> builder)
        {
            builder.ToTable(nameof(ProposalStatusHistory));
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
