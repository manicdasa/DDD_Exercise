using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.Defaults
{
    /// <summary>
    /// Represents default values related to Solution Infrastructure defaults (solution paths, folder names etc...)
    /// </summary>
    public static partial class FileSystemDefaults
    {
        #region File names
        /// <summary>
        /// Gets the name of Wwwroot folder
        /// </summary>
        public static string Wwwroot => "wwwroot";

        /// <summary>
        /// Gets the name of images folder
        /// </summary>
        public static string Images => "images";

        /// <summary>
        /// Gets the name of images folder
        /// </summary>
        public static string Documents => "documents";

        /// <summary>
        /// Gets the name of images profile pictures folder
        /// </summary>
        public static string ProfilePictures => "profile-pictures";

        /// <summary>
        /// Gets the name of booking documents folder
        /// </summary>
        public static string BookingDocuments => "booking-documents";

        #endregion

        #region Local paths

        /// <summary>
        /// Gets the name of the images folder local path
        /// </summary>
        public static string ImagesLocalPath => "wwwroot\\images\\";

        /// <summary>
        /// Gets the name of the profile-pictures folder local path
        /// </summary>
        public static string ProfilePicturesLocalPath => "wwwroot\\images\\profile-pictures\\";

        /// <summary>
        /// Gets the name of the documents folder local path
        /// </summary>
        public static string DocumentsLocalPath => "wwwroot\\documents\\";

        /// <summary>
        /// Gets the name of the booking documents folder local path
        /// </summary>
        public static string BookingDocumentsLocalPath => "wwwroot\\documents\\booking-documents\\";

        #endregion

        #region Site paths

        public static string BookingDetailsPage => "/booking/&id=%BOOKING_ID%&param=%HEAD_PROPOSAL_ID%";
        public static string ProjectDetailsPage => "/project/&id=%PROJECT_ID%";

        #endregion

        public static int GetProjectIdFromLink(string link)
        {
            link = link.Replace("/project/&id=", string.Empty);
            int.TryParse(link, out int id);

            return id;
        }
    }
}
