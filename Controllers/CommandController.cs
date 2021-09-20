using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class CommandController : ApiController
    {
        Command objCommad = new Command();
        ClsGetPendingCommandResponse objClsGetPendingCommandResponse = new ClsGetPendingCommandResponse();
        Exceptions objException = new Exceptions();

        [HttpPost]
        public ClsGetPendingCommandResponse GetPending (ClsGetPendingCommandRequest clsGetPendingCommandRequest)
        {
            try
            {
                objClsGetPendingCommandResponse = objCommad.GetPendingCommand(clsGetPendingCommandRequest);
                return objClsGetPendingCommandResponse;
            }
            catch(Exception ex)
            {
                objClsGetPendingCommandResponse.ErrorCode = "-1";
                objClsGetPendingCommandResponse.ErrorMessage=ex.Message;
                //Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsGetPendingCommandResponse;
            }
        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Command";
                objException.Url = "Command";
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
