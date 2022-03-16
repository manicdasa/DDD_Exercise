using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IPlagiarismChecker
    {
        public Task CheckForPlagiarismAsync(string baseUrl, string fileName, string filePath, string scanId);
        string CreateCopyleaksScanId(int documentId);
        int GetIdFromScanId(string scanId);
    }
}
