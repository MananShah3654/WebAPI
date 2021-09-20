using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Web_API.Request
{
    public class ClsSayHelloRequest
    {
        //public string IMEI { get; set; }
        //public string MAC { get; set; }
        //public string AppVersion { get; set; }
        //public string DeviceTime { get; set; }
        //public string TotalUser { get; set; }
        //public string TotalTransactions { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
        //public string CanteenCode { get; set; }
        

      
        public string DeviceSystemID { get; set; }

        public string DeviceDate { get; set; }

        public string FWVer { get; set; }

        public string UserCount { get; set; }

        public string TransCount { get; set; }

        public string FPCount { get; set; }

        //public string SyncDateTime { get; set; }

    }


    public class ClsSayHelloRequestCanteen
    {
        //public string IMEI { get; set; }
        //public string MAC { get; set; }
        //public string AppVersion { get; set; }
        //public string DeviceTime { get; set; }
        //public string TotalUser { get; set; }
        //public string TotalTransactions { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
        //public string CanteenCode { get; set; }



        public string DeviceSystemID { get; set; }

        public string DeviceDate { get; set; }

        public string FWVer { get; set; }

        public string UserCount { get; set; }

        public string TransCount { get; set; }

        public string FPCount { get; set; }

        public string SyncDateTime { get; set; }

        public string AppType { get; set; }
        // 1 - Meal Booking Device
        // 2 - Meal Confirm Device


    }
}