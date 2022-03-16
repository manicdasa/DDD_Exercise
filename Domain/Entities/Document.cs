using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Document
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// CopyLeaksScanId
        /// </summary>
        public string CopyLeaksScanId { get; set; }

        /// <summary>
        /// Public name of the file that is shown in the application
        /// </summary>
        public string PublicName { get; set; }

        /// <summary>
        /// The file is saved in the file system with this name
        /// </summary>
        public string PrivateName { get; set; }

        /// <summary>
        /// Local path of the file on the file system
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Flag identifying weather the document is the final version of the project
        /// </summary>
        public bool IsFinalVersion { get; set; }

        /// <summary>
        /// Date created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// File Binary
        /// </summary>
        public byte FileBinary { get; set; }

        /// <summary>
        /// Booking id
        /// </summary>
        public int? BookingId { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public virtual Booking Booking { get; set; }

        /// <summary>
        /// User that has uploaded the document (author or customer)
        /// </summary>
        public virtual ApplicationUser UploadedByUser { get; set; }

        /// <summary>
        /// PlagiarismCheckInformation 
        /// </summary>
        public virtual ICollection<PlagiarismCheckInformation> PlagiarismCheckInformation { get; set; }
    }
}
