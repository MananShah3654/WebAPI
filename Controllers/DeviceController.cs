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
    public class DeviceController : ApiController
    {
        Device objDevice = new Device();
        ClsSayHelloResponse objClsSayHelloResponse = new ClsSayHelloResponse();
        ClsGetLicenseResponse objClsGetLicenseResponse = new ClsGetLicenseResponse();
        Exceptions objException = new Exceptions();

        [HttpPost]
        public ClsSayHelloResponse SayHello(ClsSayHelloRequest clsSayHelloRequest)
        {
            try
            {
                LogManager.WriteErrorLog("Entry Say Hello");
                LogManager.WriteErrorLog(clsSayHelloRequest.DeviceSystemID + "," + clsSayHelloRequest.DeviceDate + "," + clsSayHelloRequest.FWVer + "," + clsSayHelloRequest.UserCount + "," + clsSayHelloRequest.TransCount + "," + clsSayHelloRequest.FPCount); 
                objClsSayHelloResponse = objDevice.SayHello(clsSayHelloRequest);
                LogManager.WriteErrorLog("Exit Say Hello"); 
                return objClsSayHelloResponse;
            }                                               
            catch (Exception ex)
            {                                               
                LogManager.WriteErrorLog(ex.Message);
                objClsSayHelloResponse.ErrorCode = "-1";     
                objClsSayHelloResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);

                return objClsSayHelloResponse;            
           
            }
        }

        [HttpPost]
        public ClsGetLicenseResponse GetLicense(ClsGetLicenseRequest clsGetLicenseRequest)
        {
            try
            {
                LogManager.WriteErrorLog("Entry Get License");
                objClsGetLicenseResponse = objDevice.GetLicense(clsGetLicenseRequest);
                return objClsGetLicenseResponse;
            }
            catch (Exception ex)
            {
                LogManager.WriteErrorLog(ex.Message);
                objClsGetLicenseResponse.ErrorCode = "-1";
                objClsGetLicenseResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);

                return objClsGetLicenseResponse;

            }
        }




        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Device";
                objException.Url = "Device";
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
