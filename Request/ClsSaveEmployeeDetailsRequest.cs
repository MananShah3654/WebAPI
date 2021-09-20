using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsSaveEmployeeDetailsRequest
    {
        public string DeviceSystemID { get; set; }
        public string KioskCode { get; set; }
        public string LT { get; set; }
        public string LI { get; set; }
        public string LM { get; set; }
        public string LR { get; set; }
        public string LL { get; set; }
        public string RT { get; set; }
        public string RI { get; set; }
        public string RM { get; set; }
        public string RR { get; set; }
        public string RL { get; set; }
        public string Photo { get; set; }
        public string CardNo { get; set; }
        public string CardValidity { get; set; }
    }
}