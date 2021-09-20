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

namespace Web_API.Controllers
{
    public class CanteenController : ApiController
    {
        Canteen objCanteen = new Canteen();
        ClsSayHelloResponseCanteen objClsSayHelloResponseCanteen = new ClsSayHelloResponseCanteen();
       
        Exceptions objException = new Exceptions();
         
        [HttpPost]
        public ClsSayHelloResponseCanteen SayHello(ClsSayHelloRequestCanteen clsSayHelloRequestCanteen)
        {
            try
            {
                objClsSayHelloResponseCanteen = objCanteen.SayHello(clsSayHelloRequestCanteen);
                return objClsSayHelloResponseCanteen;
            }
            catch (Exception ex)
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);

                return objClsSayHelloResponseCanteen;

            }
        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Canteen";
                objException.Url = "Canteen";
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
