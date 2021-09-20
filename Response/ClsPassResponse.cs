using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Response
{
    public class ClsSyncPassResponse
    {
        public ClsSyncPassData[] PassData { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string GatePassTime { get; set; }

    }

   

    public class ClsSyncPassData
    {
        public string EnrollID { get; set; }
        public string EmployeeName { get; set; }
        public string PermittedEnrollID { get; set; }
        public string PermittedEmployeeName { get; set; }
        public string PassID { get; set; }
        public string PassCode { get; set; }
        public string GatePassType { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

  
}