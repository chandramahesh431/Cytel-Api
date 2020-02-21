using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cytel.Top.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cytel.Top.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/S3Bucket")]
    public class S3BucketController : ControllerBase
    {
        private readonly IS3Service _service;
        
        public S3BucketController(IS3Service service)
        {
            _service = service;
        }

        [HttpPost("{bucketName}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketName) 
        {
            var response = await _service.CreateBucketAsync(bucketName);
            return Ok(response);
        }

        [HttpGet("GetFile/{bucketName}/{keyname}")]
        public async Task<IActionResult> GetObjectFromS3Async([FromRoute] string bucketName,string keyName)
        {
            var response = await _service.GetObjectFromS3Async(bucketName, keyName);
            return Ok(new { response });
        }
    }
}