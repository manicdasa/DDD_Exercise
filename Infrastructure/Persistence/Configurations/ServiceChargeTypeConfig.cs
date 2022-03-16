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
    public class ServiceChargeTypeConfig : IEntityTypeConfiguration<ServiceChargeType>
    {
        public void Configure(EntityTypeBuilder<ServiceChargeType> builder)
        {
            builder.ToTable(nameof(ServiceChargeType));
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}