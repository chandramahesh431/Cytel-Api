using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Cytel.Top.Model;
using Cytel.Top.Repository;


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
        private readonly StudyRepository customerRepository;

        /// <summary>
        /// Injecting the configuration object to the constructor
        /// </summary>
        /// <param name="configuration"></param>
        public StudyController(IConfiguration configuration)
        {
            customerRepository = new StudyRepository(configuration);
        }

       /// <summary>
       /// GET API endpoint , it returns all study details in JSON format
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("api/studies")]
        public IEnumerable<Study> Get()
        {
            IEnumerable<Study> listAll = customerRepository.FindAll();
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
            customerRepository.Add(_input);
        }
    }
}