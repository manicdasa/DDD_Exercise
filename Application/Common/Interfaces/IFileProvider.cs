using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IFileProvider
    {
        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist
        /// </summary>
        /// <param name="path">The directory to create</param>
        void CreateDirectory(string path);

        /// <summary>
        /// Creates or overwrites a file in the specified path
        /// </summary>
        /// <param name="path">The path and name of the file to create</param>
        void CreateFile(string path);

        /// <summary>
        /// Saves a file on the filePath
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<bool> SaveFile(IFormFile formFile, string filePath, string fileName);

        /// <summary>
        /// Creates Guid based on current time
        /// </summary>
        /// <returns></returns>
        Guid GetFileNameExtensionGuid();
    }
}
