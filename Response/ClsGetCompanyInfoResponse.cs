using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsGetCompanyInfoResponse
    {
        public string ServerDateTime { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}