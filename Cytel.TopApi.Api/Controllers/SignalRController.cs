using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Cytel.Top.Api.Services;
using Cytel.Top.Api.Hubs;

namespace Cytel.Top.Api.Controllers
{
    /// <summary>
    /// Api controller performing SignalR notifications operations
    /// </summary>
   
    [ApiController]
    public class SignalRController : ControllerBase
    {
        /// <summary>
        /// SignalR Repository Object
        /// </summary>
        private readonly ISignalR _signalRService;
        /// <summary>
        /// IHub Context Repository Object
        /// </summary>
        private readonly Microsoft.AspNetCore.SignalR.IHubContext<Notifications> _hub;

        /// <summary>
        /// Injecting the configuration object to the constructor
        /// </summary>
        /// <param name="hub"></param>
        /// <param name="signalRService"></param>
        public SignalRController(Microsoft.AspNetCore.SignalR.IHubContext<Notifications> hub, ISignalR signalRService)
        {
            _hub = hub;
            _signalRService = signalRService;

        }
        /// <summary>
        /// POST API endpoint , notifications can be created using this endpoint, input parameter is clientId 
        /// </summary>
        /// <param name="clientId"></param>
        [HttpPost]
        [Route("api/signalr/{clientId}")]
        public async Task<IActionResult> Send(int clientId)
        {
            // _signalR.AddData(clientId,message);
            var data =await _signalRService.GetData(clientId);
            await _hub.Clients.All.SendAsync("Send", data);
            return Ok(new { Message = "Request Completed" });
        }
    }
}