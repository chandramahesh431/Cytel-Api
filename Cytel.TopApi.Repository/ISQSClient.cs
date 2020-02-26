using System;
using System.Collections.Generic;
using System.Text;
using Cytel.Top.Model;
namespace Cytel.Top.Repository
{
       
    /// <summary>
    /// Interface created for providing data abstraction for the SQS service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISQSClient<T> where T : BaseEntity
        {
            void SendMessageToSQS(T item);
        }
}
