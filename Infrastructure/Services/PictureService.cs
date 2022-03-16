using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models.Authentication;
using System.Text.RegularExpressions;
using GhostWriter.Infrastructure.Config;

namespace GhostWriter.Infrastructure.Services
{
    public class PictureService : IPictureService
    {
        private readonly IFileProvider _fileProvider;

        public PictureService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        #region Getting picture local path/URL methods

        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        public virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }

            return lastPart;
        }

        #endregion

        /// <summary>
        /// Save picture on file system
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        public virtual void SavePictureInFile(string pictureName, List<string> folders, string mimeType, byte[] pictureBinary)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var fileFullName = $"{pictureName}.{lastPart}";

            _fileProvider.CreateDirectory(GetAbsolutePath(folders));

            folders.Add(fileFullName);
            var absolutePath = GetAbsolutePath(folders);

            File.WriteAllBytes(absolutePath, pictureBinary);
        }


        /// <summary>
        /// Loads a picture from file
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary</returns>
        public virtual string LoadPictureFromFile(List<string> folders, string pictureName, string mimeType)
        {

            var filePath = GetAbsolutePath(folders);

            if (Directory.Exists(filePath))
            {
                var lastPart = GetFileExtensionFromMimeType(mimeType);
                var fileFullName = $"{pictureName}.{lastPart}";

                folders.Add(fileFullName);
                var absolutePath = GetAbsolutePath(folders);

                return File.Exists(absolutePath) ? ConvertPictureToBase64(File.ReadAllBytes(absolutePath), mimeType) : PictureDefaults.DefaultProfilePicture;
            }

            return PictureDefaults.DefaultProfilePicture;
        }

        public virtual string GetDefaultAuthorPicture()
        {
            return PictureDefaults.DefaultProfilePicture;
        }

        public virtual string GetDefaultPicture()
        {
            return PictureDefaults.DefaultAuthorPicture;
        }

        public virtual string GetMimeTypeFromFilePath(string filePath)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var mimeType);

            return mimeType ?? MimeTypes.ImageJpeg;
        }

        public virtual PictureModel ReadPictureFromBase64(string base64)
        {
            //data:image/jpeg;base64, 
            var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);

            var match = regex.Match(base64);

            var mime = match.Groups["mime"].Value;
            var data = match.Groups["data"].Value;

            if (match.Success)
            {
                return new PictureModel()
                {
                    Type = mime,
                    Data = Convert.FromBase64String(data),
                    FormatSupported = MimeTypes.ProfilePictureCheckIfSupported(mime),
                    SuccessfullyConverted = true
                };
            }
            else
                return new PictureModel()
                {
                    Type = null,
                    Data = null,
                    FormatSupported = false,
                    SuccessfullyConverted = false
                };
        }

        public virtual string ConvertPictureToBase64(byte[] picture, string mimeType)
        {
            var encoding = "base64";

            return @$"data:{mimeType};{encoding},{Convert.ToBase64String(picture)}";

        }

        /// <summary>
        /// Returns the absolute path to the directory
        /// </summary>
        /// <param name="paths">An array of parts of the path</param>
        /// <returns>The absolute path to the directory</returns>
        public virtual string GetAbsolutePath(List<string> paths)
        {
            return Combine(paths.ToArray());
        }

        public virtual byte[] ImageToByteArray(string imageLocation)
        {
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            br.Close();
            fs.Close();
            return imageData;
        }

        #region Utilities

        /// <summary>
        /// Determines if the string is a valid Universal Naming Convention (UNC)
        /// for a server and share path.
        /// </summary>
        /// <param name="path">The path to be tested.</param>
        /// <returns><see langword="true"/> if the path is a valid UNC path; 
        /// otherwise, <see langword="false"/>.</returns>
        protected static bool IsUncPath(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out var uri) && uri.IsUnc;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Combines an array of strings into a path
        /// </summary>
        /// <param name="paths">An array of parts of the path</param>
        /// <returns>The combined paths</returns>
        public virtual string Combine(params string[] paths)
        {
            var path = Path.Combine(paths.SelectMany(p => IsUncPath(p) ? new[] { p } : p.Split('\\', '/')).ToArray());

            if (Environment.OSVersion.Platform == PlatformID.Unix && !IsUncPath(path))
                //add leading slash to correctly form path in the UNIX system
                path = "/" + path;

            return path;
        }


        #endregion
    }
}
