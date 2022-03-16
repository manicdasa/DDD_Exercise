using GhostWriter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.WebUI.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageDTO message);
    }
}
