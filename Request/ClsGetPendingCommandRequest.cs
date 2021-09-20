using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsGetPendingCommandRequest
    {
       public string DeviceSystemID { get; set; }
       public string LastProcessedCommandCode { get; set; }
    }
}