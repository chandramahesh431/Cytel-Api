using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;

namespace Cytel.Top.SQS
{
    public class SQSClient: IDisposable
    {

        // Track whether Dispose has been called.
        private bool disposed = false;

        public void SendMessageTOSQS(string messge,string deduplicationID)
        {
            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
            var queueUrl = "https://sqs.ap-south-1.amazonaws.com/066325793814/Cytelpoc.fifo";
            //var sqsRequest = new CreateQueueRequest()
            //{
            //    QueueName = "CytelSimulationQueueTest",
            //};
            //var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;
            //var myQueueQurl = createQueueResponse.QueueUrl;
            //var listQueuesRequest = new ListQueuesRequest();
            //var listQueuesResponse = sqs.ListQueuesAsync(listQueuesRequest);
            //Console.WriteLine("List of Queues");
            //foreach(var queueUrl in listQueuesResponse.Result.QueueUrls)
            //{
            //    Console.WriteLine($"Queue Url: {queueUrl}");
            //}

            var sqsmessageRequest = new SendMessageRequest()
            {
                QueueUrl = queueUrl,
                MessageBody = messge,
                MessageGroupId = "CytelMessages1",
                MessageDeduplicationId = $"CytelDedup{deduplicationID}"
            };
            sqs.SendMessageAsync(sqsmessageRequest);
        }

        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // Note disposing has been done.
                disposed = true;
            }
        }
        ~SQSClient()
        {
            Dispose(false);
        }
    }
}