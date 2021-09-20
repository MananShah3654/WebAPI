using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Request
{
    public class ClsConfirmRequest
    {
        public string MastCode { get; set; }
        public string IMEI { get; set; }
        public string EmpCode { get; set; }
        public string EmpID { get; set; }
        public string BookingDate { get; set; }
        public string TokenNo { get; set; }
        public string MealType { get; set; }
        public string ItemCode { get; set; }
        //public string Company { get; set; }
        //public string IsActive { get; set; }

        //public string EmpName { get; set; }
        //public string MealName { get; set; }
        public string CanteenCode { get; set; }
        public string OrderID { get; set; }

    }
}