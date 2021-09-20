using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsSaveSuccessRequest
    {
        public string DeviceSystemID { get; set; }
        public string EmpID { get; set; }
        public string InOutTime { get; set; }
        public string Photo { get; set; }
    }
}