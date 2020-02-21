using Cytel.Top.Api.Models;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Services
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);
        Task<string> GetObjectFromS3Async(string bucketName, string keyName);
    }
}