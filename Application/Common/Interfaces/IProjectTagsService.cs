using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IProjectTagsService
    {
        public Task<object> AddCustomField(string CustomFieldValue, object Entity, CancellationToken cancellationToken);
        public Task<List<ExpertiseArea>> AddNonExistingExpertiseAreas(List<LookupSingleResultModel> expertiseAreas, CancellationToken cancellationToken);
        public Task<List<KindOfWork>> AddNonExistingKindOfWorks(List<LookupSingleResultModel> kindOfWorks, CancellationToken cancellationToken);
    }
}
