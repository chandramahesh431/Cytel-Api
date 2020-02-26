using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Models
{
    /// <summary>
    /// Response Class structure for S3 Bucket api endpoints
    /// </summary>
    public class S3Response
    {
        /// <summary>
        /// Stores the Status code from the API
        /// </summary>
        public HttpStatusCode Status { get; set; }

       /// <summary>
       /// Respose Message from the API
       /// </summary>
        public string  Message { get; set; }
    }
}
