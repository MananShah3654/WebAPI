using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Request
{
    public class ClsSyncPassRequest
    {
        public string DeviceSystemID { get; set; }
        public string DeviceDate { get; set; }
        public string LastSyncDateTime { get; set; }
        public string Flag { get; set; }

    }
}