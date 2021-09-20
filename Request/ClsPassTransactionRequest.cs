using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Request
{
    public class ClsPassTransactionRequest
    {
        public string IMEI		   { get; set; }
        public string EnrollID	   { get; set; }
        public string EmpID		   { get; set; }
        public string In_out_time  { get; set; }
        public string GatePassType { get; set; }
        public string PassCode	   { get; set; }
        public string Flag		   { get; set; }
        public string AuthType     { get; set; }
        public string RejectType { get; set; }
      
    }
}