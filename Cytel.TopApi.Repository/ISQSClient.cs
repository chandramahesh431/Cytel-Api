using System;
using System.Collections.Generic;
using System.Text;
using Cytel.Top.Model;
namespace Cytel.Top.Repository
{
        public interface ISQSClient<T> where T : BaseEntity
        {
            void SendMessageToSQS(T item);
        }
}
