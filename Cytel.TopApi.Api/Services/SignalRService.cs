using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Services
{
    public class SignalRService:ISignalR
    {
        List<notification> notificationsList = new List<notification>();
        public SignalRService()
        {
            notificationsList.Add(new notification() { id = 1, message = "Completed Trial Study" });
            notificationsList.Add(new notification() { id = 2, message = "Failed Trial Study" });
            notificationsList.Add(new notification() { id = 3, message = "Completed Trial Study" });
            notificationsList.Add(new notification() { id = 4, message = "Completed Trial Study" });
        }
        public List<notification> GetData(int id)
        {
            return notificationsList.FindAll(x=>x.id==id);
        }

        public void AddData(int id,string message)
        {
            notificationsList.Add(new notification() { id = id, message = message });
        }
    }


    public class notification
    {
        public int id { set; get; }
        public string message { set; get; }
    }
}
