using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class UserRoleData
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Total amount spent (fulfilled if the user has the Customer role assigned)
        /// </summary>
        public decimal TotalSpent { get; set; }

        /// <summary>
        /// Total count of projects the customer has posted to the website (fulfilled if the user has the Customer role assigned)
        /// </summary>
        public int JobsPostedCnt { get; set; }

        /// <summary>
        /// IBAN 
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// Author's paypal account email
        /// </summary>
        public string PaypalEmail { get; set; }

        /// <summary>
        /// Paypal Payer ID needed to make payout to author's Paypal account
        /// </summary>
        public string PaypalPayerID { get; set; }

        /// <summary>
        /// Flag detecting weather a customer has ever paid for a project (fulfilled if the user has the Customer role assigned)
        /// </summary>
        public bool PaymentVerified { get; set; }

        /// <summary>
        /// Price Per Page
        /// </summary>
        public decimal PricePerPage { get; set; }

        /// <summary>
        /// Average Price per Page
        /// </summary>
        public decimal? AvgPricePerPage { get; set; }
        
        /// <summary>
        /// Defines weather direct booking is enabled
        /// </summary>
        public bool DirectBooking { get; set; }

        /// <summary>
        /// Number of pages the ghrostwriter can write per day
        /// </summary>
        public int PagesPerDay { get; set; }

        /// <summary>
        /// Profile Introduction
        /// </summary>
        public string ProfileIntroduction { get; set; }

        /// <summary>
        /// Description 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Application User Role reference
        /// </summary>
        public virtual ApplicationUserRole ApplicationUserRole { get; set; }

        /// <summary>
        /// Picture
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Highest Degree of ghostwriter
        /// </summary>
        public virtual Degree HighestDegree { get; set; }

        /// <summary>
        /// Languages
        /// </summary>
        public virtual ICollection<Language> Languages { get; set; }

        /// <summary>
        /// Kind of Works
        /// </summary>
        public virtual ICollection<KindOfWork> KindOfWorks { get; set; }

        /// <summary>
        /// Expertise Areas
        /// </summary>
        public virtual ICollection<ExpertiseArea> ExpertiseAreas { get; set; }

        /// <summary>
        /// Buzzwords
        /// </summary>
        public virtual ICollection<Buzzword> Buzzwords { get; set; }
    }
}
