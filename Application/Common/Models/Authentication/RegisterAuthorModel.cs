using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Models
{
    public class RegisterAuthorInputModel : RegisterAuthorModel
    {
        public string PaypalPayerID { get; set; }
        public string PaypalEmail { get; set; }
    }
    public class RegisterAuthorModel: RegisterModel
    {
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string LastName { get; set; }
        public string IBAN { get; set; }
        public string PaypalCode { get; set; }
        [Required]
        public string ProfileIntroduction { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public decimal PricePerPage { get; set; }
        [Required]
        public bool DirectBooking { get; set; }

        // TODO: Wording on the page makes no sense 
        // Either go with pages per day or days per page
        // Just make sure it's clear
        [Required]
        [Range(1, int.MaxValue)]
        public int PagesPerDay { get; set; }
        public string ProfilePicture { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int HighestDegreeId { get; set; }
        [Required]
        public List<int> LanguageIds { get; set; }
        [Required]
        public List<LookupSingleResultModel> KindOfWorks { get; set; }
        [Required]
        public List<LookupSingleResultModel> ExpertiseAreas { get; set; }
    }
}
