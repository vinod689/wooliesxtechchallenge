using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesXTechChallenge.Application.CoreLogger
{
    public class LogDetailModel
    {
        public LogDetailModel()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        //Where
        public string Location { get; set; }

        //Who
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        //Everything else

        public Exception Exception { get; set; }
        public CustomExceptionModel CustomException { get; set; }

        public string CorrelationId { get; set; }

        public Dictionary<string,object> AdditionalInfo { get; set; }

    }
}
