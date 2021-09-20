using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsSaveRejectRequest
    {
        public string DeviceSystemID { get; set; }
        public string EmpID { get; set; }
        public string CardNo { get; set; }
        public string RejectType { get; set; }
        public string CardType { get; set; }
        public string PunchDateTime { get; set; }
        public string OfflineID { get; set; }
        public string Photo { get; set; }
    }
}