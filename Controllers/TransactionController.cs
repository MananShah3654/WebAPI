using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class TransactionController : ApiController
    {
        Transaction objTransaction = new Transaction();
        ClsTransactionResponse objClsTransactionResponse = new ClsTransactionResponse();
        Exceptions objException = new Exceptions();

        [HttpPost]
        public ClsTransactionResponse Success(ClsSaveSuccessRequest clsSaveSuccessRequest)
        {
            try
            {
                objClsTransactionResponse = objTransaction.SaveSuccess(clsSaveSuccessRequest);
                return objClsTransactionResponse;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY") == true)
                {
                    objClsTransactionResponse.ErrorCode = "0";
                    objClsTransactionResponse.ErrorMessage = "SUCCESS";
                }
                else
                {
                    objClsTransactionResponse.ErrorCode = "-1";
                    objClsTransactionResponse.ErrorMessage = ex.Message;
                    //Save Exception
                    Exception(ex.Message, ex.StackTrace);
                }
                 return objClsTransactionResponse;
            }
        }

        [HttpPost]
        public ClsTransactionResponse Reject(ClsSaveRejectRequest clsSaveRejectRequest)
        {
            try
            {
                objClsTransactionResponse = objTransaction.SaveReject(clsSaveRejectRequest);
                return objClsTransactionResponse;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY") == true)
                {
                    objClsTransactionResponse.ErrorCode = "0";
                    objClsTransactionResponse.ErrorMessage = "SUCCESS";

                }
                else
                {
                    objClsTransactionResponse.ErrorCode = "-1";
                    objClsTransactionResponse.ErrorMessage = ex.Message;
                    //Save Exception
                    Exception(ex.Message, ex.StackTrace);
                }
               

                return objClsTransactionResponse;

            }
        }

        

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Transaction";
                objException.Url = "Transaction";
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
