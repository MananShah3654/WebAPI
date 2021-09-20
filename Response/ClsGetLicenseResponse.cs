using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Response
{
    public class ClsGetLicenseResponse
    {
        private string _ErrorCode;

        public string ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

    
        private string _License;

        public string License
        {
            get { return _License; }
            set { _License = value; }
        }

          public string ErrorMessage { get; set; }
        }
}
    
