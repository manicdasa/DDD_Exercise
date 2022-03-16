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
    public class DocumentConfig : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable(nameof(Document));
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.UploadedByUser)
                 .WithMany(x => x.Documents);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
