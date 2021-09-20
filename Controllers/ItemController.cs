using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Response;
using Web_API.Request;
using Web_API.Common;
using Web_API.Models;
using System.Data;

namespace Web_API.Controllers
{
    public class ItemController : ApiController
    {
        Item objMeal = new Item();
        ClsSyncItemMasterResponse objClsSyncItemMasterResponse = new ClsSyncItemMasterResponse();
        Exceptions objException = new Exceptions();

        [HttpPost]
        public ClsSyncItemMasterResponse Sync(ClsSyncItemMasterRequest clsSyncItemMasterRequest)
        {
            try
            {
                objClsSyncItemMasterResponse = objMeal.SyncItemMaster(clsSyncItemMasterRequest);
                return objClsSyncItemMasterResponse;
            }
            catch (Exception ex)
            {
                objClsSyncItemMasterResponse.ErrorCode = "-1";
                objClsSyncItemMasterResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsSyncItemMasterResponse;

            }

        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Item";
                objException.Url = "Item";
                objException.Message = exceptionMessage;
                objException.StackTrace = exceptionStackTrace;
                objException.UserCode = 0;
                objException.UserName = "";
                objException.Save();
            }
            catch (Exception)
            {
                //TBD
            }
        }
        #endregion
    }
}
