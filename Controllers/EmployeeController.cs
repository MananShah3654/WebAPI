using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class EmployeeController : ApiController
    {
        Employee objEmployee = new Employee();
        ClsGetEmployeeDetailsResponse objClsGetEmployeeDetailsResponse = new ClsGetEmployeeDetailsResponse();
        ClsSaveEmployeeDetailsResponse objClsSaveEmployeeDetailsResponse = new ClsSaveEmployeeDetailsResponse();
        Exceptions objException = new Exceptions();
        
        [HttpPost]
        public ClsGetEmployeeDetailsResponse GetDetails(ClsGetEmployeeDetailsRequest clsGetEmployeeDetailsRequest)
        {
            try
            {
                objClsGetEmployeeDetailsResponse = objEmployee.GetEmployeeDetails(clsGetEmployeeDetailsRequest);
                return objClsGetEmployeeDetailsResponse;
            }
            catch (Exception ex)
            {
                objClsGetEmployeeDetailsResponse.ErrorCode = "-1";
                objClsGetEmployeeDetailsResponse.ErrorMessage = ex.Message;
                //Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsGetEmployeeDetailsResponse;
            }
        }

        [HttpPost]
        public ClsSaveEmployeeDetailsResponse SaveDetails(ClsSaveEmployeeDetailsRequest clsSaveEmployeeDetailsRequest)
        {
            try
            {
                objClsSaveEmployeeDetailsResponse = objEmployee.SaveEmployeeDetails(clsSaveEmployeeDetailsRequest);
                return objClsSaveEmployeeDetailsResponse;
            }
            catch (Exception ex)
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = ex.Message;
                //Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsSaveEmployeeDetailsResponse;
            }
        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Employee";
                objException.Url = "Employee";
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
