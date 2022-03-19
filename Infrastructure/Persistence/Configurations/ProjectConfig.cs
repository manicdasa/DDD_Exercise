﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Infrastructure.Configurations
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Deadline);
            builder.Property(x => x.Description);
            builder.Property(x => x.IsPublished);
            builder.Property(x => x.LastUpdate);
            builder.Property(x => x.MaxBudget);
            builder.Property(x => x.PagesNo);
            builder.Property(x => x.PlannedBudget);
            builder.Property(x => x.DateCreated);
            builder.Property(x => x.CustomerId);
            builder.Property(x => x.KindOfWorkId);
            builder.Property(x => x.ProjectStatus);
            builder.Property(x => x.ProjectTopic);
            

            builder.HasOne(x => x.KindOfWork)
                .WithMany(x => x.Projects)
                .HasForeignKey(ur => ur.KindOfWorkId).IsRequired();

            builder
           .HasMany(x => x.Buzzwords)
           .WithMany(x => x.Projects);

            builder
            .HasMany(x => x.ExpertiseAreas)
            .WithMany(x => x.Projects);

            builder
            .HasMany(x => x.ServiceCharges)
            .WithMany(x => x.Projects);

            builder.Navigation(x => x.Buzzwords);
            builder.Navigation(x => x.ServiceCharges);
            builder.Navigation(x => x.ExpertiseAreas);
        }
    }
}
