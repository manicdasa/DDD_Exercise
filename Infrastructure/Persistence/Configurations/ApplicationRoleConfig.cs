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
    public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("AspNetRoles");
            builder.HasKey(x => x.Id);

            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
