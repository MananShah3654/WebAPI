using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Request
{
    public class ClsSyncMealMasterRequest
    {
        public string SyncDate { get; set; }
        public string MealMastCode { get; set; }
        public string CanteenCode { get; set; }
        public string IMEI { get; set; }
        public string UserCode { get; set; }
        public string Company { get; set; }
    }
}