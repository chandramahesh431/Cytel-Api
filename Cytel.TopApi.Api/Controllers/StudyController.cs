using Cytel.Top.Api.Interfaces;
using Cytel.Top.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


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
        /// Injecting the configuration object to the constructor
        /// </summary>
        /// <param name="configuration"></param>
        public StudyController(IStudyService studyService)
        {
            _studyService = studyService;
        }

       /// <summary>
       /// GET API endpoint , it returns all study details in JSON format
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("api/studies")]
        public IEnumerable<Study> Get()
        {
            IEnumerable<Study> listAll = _studyService.FindAll();
            return listAll;
        }

        /// <summary>
        /// POST API endpoint , studies can be created using this endpoint, input parameter is study object 
        /// </summary>
        /// <param name="_input"></param>
        [HttpPost]
        [Route("api/studies")]
        public void Post(Study _input)
        {
            _studyService.Add(_input);
        }
    }
}