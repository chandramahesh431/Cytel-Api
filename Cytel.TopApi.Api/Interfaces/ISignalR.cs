using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cytel.Top.Api.Hubs;

namespace Cytel.Top.Api.Services
{
    /// <summary>
    /// Interface created for SignalR Notifications operations, Abstraction layer for SignalR Notifications Service
    /// </summary>
    public interface ISignalR
    {
        /// <summary>
        /// Interface Method for get notifications
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<notification> GetData(int id);
        /// <summary>
        /// Interface Method for add notifications
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        void AddData(int id,string message);
    }
}
