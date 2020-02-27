using Cytel.Top.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Controllers
{
    /// <summary>
    /// Api controller performing S3 Bucket operations
    /// </summary>
    [Produces("application/json")]
    [Route("api/S3Bucket")]
    public class S3BucketController : ControllerBase
    {
        private readonly IS3Service _service;
        
        public S3BucketController(IS3Service service)
        {
            _service = service;
        }

        /// <summary>
        /// Function to create an S3 bucket in AWS cloud, bucket name can be
        /// provided as function parameter
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        [HttpPost("{bucketName}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketName) 
        {
            var response = await _service.CreateBucketAsync(bucketName);
            return Ok(response);
        }

        /// <summary>
        /// Function Reads the S3 object based on the key provided.
        /// </summary>
        /// <param name="bucketName"></param> S3 Bucket name
        /// <param name="keyName"></param> Key name in the S3 bucket
        /// <returns></returns>
        [HttpGet("GetFile/{bucketName}/{keyname}")]
        public async Task<IActionResult> GetObjectFromS3Async([FromRoute] string bucketName,string keyName)
        {
            var response = await _service.GetObjectFromS3Async(bucketName, keyName);
            return Ok(new { response });
        }
    }
}