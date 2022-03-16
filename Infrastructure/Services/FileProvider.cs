using GhostWriter.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GhostWriter.Infrastructure.Services
{
    public class FileProvider : IFileProvider
    {
        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist
        /// </summary>
        /// <param name="path">The directory to create</param>
        public virtual void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Creates or overwrites a file in the specified path
        /// </summary>
        /// <param name="path">The path and name of the file to create</param>
        public virtual void CreateFile(string path)
        {
            if (File.Exists(path))
                return;

            var fileInfo = new FileInfo(path);
            CreateDirectory(fileInfo.DirectoryName);

            using (File.Create(path))
            {
            }
        }

        public virtual async Task<bool> SaveFile(IFormFile formFile, string filePath, string fileName)
        {
            if (formFile.Length > 0)
            {
                CreateDirectory(filePath);
                var fullPath = Path.Combine(filePath, fileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await formFile.CopyToAsync(stream);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates Guid based on current time
        /// </summary>
        /// <returns></returns>
        public virtual Guid GetFileNameExtensionGuid()
        {
            var bytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
            Array.Resize(ref bytes, 16);

            return new Guid(bytes);
        }
    }
}
