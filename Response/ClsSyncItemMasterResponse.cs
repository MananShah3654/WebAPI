using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Response
{
    public class ClsSyncItemMasterResponse
    {
        public ClsSyncItemMasterData[] SyncItemMasterData { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ClsSyncItemMasterData
    {
        public string MastCode { get; set; }
        public string ItemCode { get; set; }
        public string MastName { get; set; }
        public string ItemPrice { get; set; }
        public string IsActive { get; set; }
        public string CanteenCode { get; set; }
        public string MinimumStock { get; set; }
        public string MealCode { get; set; }
        public string IsQuantity { get; set; }
    }

}