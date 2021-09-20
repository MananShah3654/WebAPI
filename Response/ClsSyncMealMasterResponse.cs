using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Response
{
    public class ClsSyncMealMasterResponse
    {
        public ClsSyncMealMasterRequestData[] SyncMealMasterData { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
      
    }
    public class ClsSyncMealMasterRequestData
    {
        public string MastCode { get; set; }
        public string MastName { get; set; }
        public string CodeType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TokenNo { get; set; }
        public string IsActive { get; set; }
        public string CanteenCode { get; set; }
      
    }

}