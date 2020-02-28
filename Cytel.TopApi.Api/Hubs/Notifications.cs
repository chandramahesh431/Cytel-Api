using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cytel.Top.Api.Services;

namespace Cytel.Top.Api.Hubs
{
    public class Notifications:Hub
    {
        private readonly ISignalR _signalR;

        public Notifications(ISignalR signalR)
        {
            _signalR = signalR;
        }

        public async  Task<List<notification>> GetData(int clientId)
        {
            var response=await _signalR.GetData(clientId);
            return response;
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);           
        }

    }
}
