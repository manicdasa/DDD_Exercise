using System;
using System.Collections.Generic;

namespace GhostWriter.Application.Common.Models
{
    public class OutputModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ExtendedOutputModel<T> : OutputModel
    {
        public T AdditionalInformation { get; set; }
        public List<int> AdditionalEntityKeys { get; set; }
    }

    public class ExtendedOutputModelTemp<T> : OutputModel
    {
        public int AdditionalInformation { get; set; }
        public List<T> AdditionalInformationNotification { get; set; }
    }

    public class ExtendedOutputModelList<T> : OutputModel
    {
        public List<T> AdditionalInformation { get; set; }
    }

    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }
    }
}
