using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Response;
using Web_API.Request;
using System.Data;
using Web_API.Common;
using Web_API.Models;
using WebGrease;


namespace Web_API.Controllers
{
    public class PassController : ApiController
    {
        Pass objPass = new Pass();
        ClsSyncPassResponse objClsSyncPassResponse = new ClsSyncPassResponse();
        ClsPassTransactionResponse objClsPassTransactionResponse = new ClsPassTransactionResponse();
        Exceptions objException = new Exceptions();


        [HttpPost]
        public ClsSyncPassResponse Sync(ClsSyncPassRequest ClsPassRequest)
        {
            try
            {
                objClsSyncPassResponse = objPass.GetPassRequest(ClsPassRequest);
                return objClsSyncPassResponse;
            }
            catch (Exception ex)
            {
                LogManager.WriteErrorLog(ex.Message);
                objClsSyncPassResponse.ErrorCode = "-1";
                objClsSyncPassResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);

                return objClsSyncPassResponse;

            }
        }

        [HttpPost]
        public ClsPassTransactionResponse Transaction(ClsPassTransactionRequest clsPassTransactionRequest)
        {
            try
            {
                objClsPassTransactionResponse = objPass.PassTransaction(clsPassTransactionRequest);
                return objClsPassTransactionResponse;
            }
            catch (Exception ex)
            {
                LogManager.WriteErrorLog(ex.Message);
                objClsPassTransactionResponse.ErrorCode = "-1";
                objClsPassTransactionResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);

                return objClsPassTransactionResponse;

            }
        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Pass";
                objException.Url = "Pass";
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