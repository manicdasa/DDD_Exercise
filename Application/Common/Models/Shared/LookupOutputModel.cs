using System.Collections.Generic;

namespace GhostWriter.Application.Common.Models.Shared
{
    public class LookupOutputModel : OutputModel
    {
        public List<LookupSingleResultModel> SearchResult { get; set; }
    }
}
