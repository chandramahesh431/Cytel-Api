using Cytel.Top.Api.Interfaces;
using Cytel.Top.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Cytel.Top.Api.Services;
using Cytel.Top.Api.Hubs;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Controllers
{
    /// <summary>
    /// API Controller class for Performming GET /POST actions
    /// </summary>
    [ApiController]
    public class StudyController : ControllerBase
    {
        /// <summary>
        /// Study Repository Object
        /// </summary>
        private readonly IStudyService _studyService;
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
        /// <param name="configuration"></param>
        public StudyController(IStudyService studyService, Microsoft.AspNetCore.SignalR.IHubContext<Notifications> hub, ISignalR signalRService)
        {
            _studyService = studyService;
            _hub = hub;
            _signalRService = signalRService;
        }

       /// <summary>
       /// GET API endpoint , it returns all study details in JSON format
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("api/studies")]
        public async  Task<IActionResult> Get()
        {
            var listAll =await _studyService.FindAll();
            return Ok(listAll);
        }

        /// <summary>
        /// POST API endpoint , studies can be created using this endpoint, input parameter is study object 
        /// </summary>
        /// <param name="_input"></param>
        [HttpPost]
        [Route("api/studies")]
        public async Task<IActionResult> Post(Study _input)
        {
           // var data = _signalRService.GetData(1);
           await _hub.Clients.All.SendAsync("Send", new List<notification>() { new notification() {id=1,message= _input.StudyName+" started processing" } } );
           await _studyService.Add(_input);
            return Ok(_input);
           
        }
    }
}