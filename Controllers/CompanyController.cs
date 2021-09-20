using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class CompanyController : ApiController
    {
        Company objCompany = new Company();
        ClsGetCompanyInfoResponse objClsGetCompanyInfoResponse = new ClsGetCompanyInfoResponse();
        Exceptions objException = new Exceptions();

        [HttpPost]
        public ClsGetCompanyInfoResponse GetInfo(ClsGetCompanyInfoRequest clsGetCompanyInfoRequest)
        {
            try
            {
                objClsGetCompanyInfoResponse = objCompany.GetCompanyInfo(clsGetCompanyInfoRequest);
                return objClsGetCompanyInfoResponse;
            }
            catch (Exception ex)
            {
                objClsGetCompanyInfoResponse.ErrorCode = "-1";
                objClsGetCompanyInfoResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsGetCompanyInfoResponse;

            }

        }

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Company";
                objException.Url = "Company";
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
