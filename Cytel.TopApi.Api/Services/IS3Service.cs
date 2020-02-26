using Cytel.Top.Api.Models;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Services
{
    /// <summary>
    /// Interface created for S3 operations, Abstraction layer for S3 Service
    /// </summary>
    public interface IS3Service
    {
        /// <summary>
        /// Interface Method for creating a S3 bucket
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task<S3Response> CreateBucketAsync(string bucketName);

        /// <summary>
        /// Interface Method for reading an Object from S3
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        Task<string> GetObjectFromS3Async(string bucketName, string keyName);
    }
}