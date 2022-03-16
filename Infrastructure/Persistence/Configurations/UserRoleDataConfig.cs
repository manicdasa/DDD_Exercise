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
    public class UserRoleDataConfig : IEntityTypeConfiguration<UserRoleData>
    {
        public void Configure(EntityTypeBuilder<UserRoleData> builder)
        {
            builder.ToTable(nameof(UserRoleData));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
