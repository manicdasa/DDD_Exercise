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
    public class ApplicationUserRoleConfig : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable(nameof(ApplicationUserRole));
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x => x.ApplicationUser)
              .WithMany(x => x.UserRoles)
              .HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}
