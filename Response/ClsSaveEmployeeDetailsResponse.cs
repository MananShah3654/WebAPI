using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsSaveEmployeeDetailsResponse
    {
        public string MastCode { get; set; }
        public string EmpID { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}