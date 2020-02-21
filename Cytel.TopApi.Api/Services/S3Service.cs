using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Cytel.Top.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;
        public S3Service(IAmazonS3 client)
        {
            _client = client;

        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                if (await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName) == false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };
                    var response = await _client.PutBucketAsync(putBucketRequest);
                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                    };
                }
            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Status = e.StatusCode,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                return new S3Response
                {
                    Status = System.Net.HttpStatusCode.InternalServerError,
                    Message = e.Message
                };
            }

            return new S3Response
            {
                Message = "Something Went Wrong",
                Status = HttpStatusCode.InternalServerError
            };
        }

        public async Task<string> GetObjectFromS3Async(string bucketName, string keyName)
        {
      
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                string responseBody;
                using (var response = await _client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    var title = response.Metadata["x-amz-meta-title"];
                    var contentType = response.Headers["Content-Type"];
                    Console.WriteLine($"Title: {title}");
                    Console.WriteLine($"Content-Type: {contentType}");
                    responseBody = reader.ReadToEnd();
                }
                return responseBody;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
