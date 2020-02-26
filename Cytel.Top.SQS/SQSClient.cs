using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;

namespace Cytel.Top.SQS
{
   /// <summary>
   /// SQS Client class used for sending messages to SQS 
   /// </summary>
    public class SQSClient: IDisposable
    {

        private bool disposed = false;

        /// <summary>
        /// Function sends message to SQS queue
        /// </summary>
        /// <param name="messge"></param>
        /// <param name="deduplicationID"></param>
        public void SendMessageTOSQS(string messge,string deduplicationID)
        {
            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
            var queueUrl = "https://sqs.ap-south-1.amazonaws.com/066325793814/Cytelpoc.fifo";

            var sqsmessageRequest = new SendMessageRequest()
            {
                QueueUrl = queueUrl,
                MessageBody = messge,
                MessageGroupId = "CytelMessages1",
                MessageDeduplicationId = $"CytelDedup{deduplicationID}"
            };
            sqs.SendMessageAsync(sqsmessageRequest);
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

        /// <summary>
        /// Destructor to dispose the method
        /// </summary>
        ~SQSClient()
        {
            Dispose(false);
        }
    }
}