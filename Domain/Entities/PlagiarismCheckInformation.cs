using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class PlagiarismCheckInformation
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Equals to parameter Copyleaks.SDK.V3.API.Models.Responses.Result.IdenticalWords
        /// </summary>
        public uint IdenticalWords { get; set; }

        /// <summary>
        /// TotalExcluded words from scanning in the document
        /// </summary>
        public uint TotalExcluded { get; set; }

        /// <summary>
        /// TotalWordsScanned
        /// </summary>
        public uint TotalWordsScanned { get; set; }

        /// <summary>
        /// Equals to parameter Copyleaks.SDK.V3.API.Models.Responses.Result.MinorChangedWords
        /// </summary>
        public uint MinorChangedWords { get; set; }

        /// <summary>
        /// Equals to parameter Copyleaks.SDK.V3.API.Models.Responses.Result.RelatedMeaningWords
        /// </summary>
        public uint RelatedMeaningWords { get; set; }

        /// <summary>
        /// Equals to parameter Copyleaks.SDK.V3.API.Models.Responses.Result.AggregatedScore
        /// </summary>
        public double AggregatedScore { get; set; }

        /// <summary>
        /// Credits calculated by Copyleaks
        /// </summary>
        public uint Credits { get; set; }

        /// <summary>
        /// Document id
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Document
        /// </summary>
        public virtual Document Document { get; set; }
    }
}
