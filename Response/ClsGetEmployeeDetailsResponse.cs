using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsGetEmployeeDetailsResponse
    {
        public string KioskCode { get; set; }
        public string EnrollID { get; set; }
        public string ReferenceID { get; set; }
        public string Name { get; set; }
        public string EntityType { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string CompanyLocation { get; set; }
        public string Company { get; set; }
        public string EntityCategory { get; set; }
        public string CardValidity { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

    }
}