using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class UserDownloadController : ApiController
    {
        UserDownload objCommad = new UserDownload();
        ClsGetPendingUserDownloadResponse objClsGetPendingUserDownloadResponse = new ClsGetPendingUserDownloadResponse();
        Exceptions objException = new Exceptions();

        [HttpPost]
        public ClsGetPendingUserDownloadResponse GetPending(ClsGetPendingUserDownloadRequest clsGetPendingUserDownloadRequest)
        {
            try
            {
                objClsGetPendingUserDownloadResponse = objCommad.GetPendingUserDownload(clsGetPendingUserDownloadRequest);
                return objClsGetPendingUserDownloadResponse;
            }
            catch (Exception ex)
            {
                objClsGetPendingUserDownloadResponse.ErrorCode = "-1";
                objClsGetPendingUserDownloadResponse.ErrorMessage = ex.Message;
                //Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsGetPendingUserDownloadResponse;
            }
        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "UserDownload";
                objException.Url = "UserDownload";
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
