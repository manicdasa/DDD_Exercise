using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GhostWriter.Domain.Entities
{
    public interface IProjectFactory
    {
        Task<Project> Create(string description, KindOfWork kindOfWork, IReadOnlyList<ExpertiseArea> expertiseAreas, ApplicationUser customer, Language language, Degree minDegree, decimal pricePerPage, string projectTopic, int pagesNo, bool isPublished, DateTime deadline);
    }
}