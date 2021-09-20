using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Request
{
    public class ClsGetMealBookingRequest
    { 
        public string IMEI { get; set; }
        public string CanteenCode { get; set; }
        public string MealBookCode { get; set; }
        public string ItemCode { get; set; }
        public string SyncDate { get; set; }
      
    }
}