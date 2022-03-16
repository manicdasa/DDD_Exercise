using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Picture
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// SEO friendly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Local path to the file
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Local path to the file
        /// </summary>
        public string PictureFileName { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Picture binary
        /// </summary>
        public byte[] BinaryData { get; set; }
    }
}
