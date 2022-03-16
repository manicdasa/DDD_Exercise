using GhostWriter.Application.DTOs;
using System.Collections.Generic;

namespace GhostWriter.Application.Common.Models
{ 
    public class ResponseWithPayload<T> : BasicResponse
    {
        public T SuccessPayload { get; set; }
        public List<NotificationSignalRDTO> Notifications { get; set; }
    }
}
