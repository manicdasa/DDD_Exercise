using GhostWriter.Application.Common.Models.Authentication;
using GhostWriter.Domain.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IPictureService
    {
        string GetMimeTypeFromFilePath(string filePath);
        string LoadPictureFromFile(List<string> folders, string pictureName, string mimeType);
        string GetDefaultAuthorPicture();

        string GetDefaultPicture();
        void SavePictureInFile(string pictureName, List<string> folders, string mimeType, byte[] pictureBinary);
        PictureModel ReadPictureFromBase64(string base64);
        string ConvertPictureToBase64(byte[] picture, string mimeType);

        byte[] ImageToByteArray(string imageLocation);
    }
}
