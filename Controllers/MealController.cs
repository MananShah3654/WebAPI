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
    public class MealController : ApiController
    {
        Meal objMeal = new Meal();
        ClsMealBookingResponse objClsMealBookingResponse = new ClsMealBookingResponse();
        ClsSyncMealMasterResponse objClsSyncMealMasterResponse = new ClsSyncMealMasterResponse();
        ClsGetMealBookingResponse objClsGetMealBookingResponse = new ClsGetMealBookingResponse();
        ClsConfirmResponse objClsConfirmResponse = new ClsConfirmResponse();
        Exceptions objException = new Exceptions();

        public ClsMealBookingResponse Booking (ClsMealBookingRequest clsMealBookingRequest )
        {
            try
            {
                objClsMealBookingResponse = objMeal.MealBooking(clsMealBookingRequest);
                return objClsMealBookingResponse;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                if (str.StartsWith("This Meal already exists..!"))
                {
                    char[] spearator ={'!'};
                   objClsMealBookingResponse.MastCode= str.Split(spearator)[1];

                    objClsMealBookingResponse.ErrorCode = "0";
                    objClsMealBookingResponse.ErrorMessage = ex.Message;
                }
                else
                {
                    objClsMealBookingResponse.ErrorCode = "-1";
                    objClsMealBookingResponse.ErrorMessage = ex.Message;

                }
               
                //Save Exception
                Exception(ex.Message, ex.StackTrace);

                return objClsMealBookingResponse;
            }
            

        }
        [HttpPost]
        public ClsSyncMealMasterResponse Sync (ClsSyncMealMasterRequest clsSyncMealMasterRequest)
        {
            try
            {
                objClsSyncMealMasterResponse = objMeal.SyncMealMaster(clsSyncMealMasterRequest);
                return objClsSyncMealMasterResponse;
            }
            catch (Exception ex)
            {
                objClsSyncMealMasterResponse.ErrorCode = "-1";
                objClsSyncMealMasterResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsSyncMealMasterResponse;
            
            }
          
        }
        
        [HttpPost]
        public ClsGetMealBookingResponse GetBooking(ClsGetMealBookingRequest clsGetMealBookingRequest)
        {
            try
            {
                objClsGetMealBookingResponse = objMeal.GetMealBooking(clsGetMealBookingRequest);
                return objClsGetMealBookingResponse;
            }
            catch (Exception ex)
            {
                objClsGetMealBookingResponse.ErrorCode = "-1";
                objClsGetMealBookingResponse.ErrorMessage = ex.Message;
                //Save Exception
                Exception(ex.Message, ex.StackTrace);
                return objClsGetMealBookingResponse;

            }

        }
        [HttpPost]
        public ClsConfirmResponse Confirm(ClsConfirmRequest clsConfirmRequest)
        {
            try
            {
                objClsConfirmResponse = objMeal.OrderConfirm(clsConfirmRequest);
                return objClsConfirmResponse;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY") == true)
                {
                    objClsConfirmResponse.ErrorCode = "0";
                    objClsConfirmResponse.ErrorMessage = "SUCCESS";

                }
                else
                {
                    objClsConfirmResponse.ErrorCode = "-1";
                    objClsConfirmResponse.ErrorMessage = ex.Message;
                    //Save Exception
                    Exception(ex.Message, ex.StackTrace);
                }
                return objClsConfirmResponse;
            }


        }
       

        #region User Defined Functions
        private void Exception(string exceptionMessage, string exceptionStackTrace)
        {
            try
            {
                objException.Page = "Meal";
                objException.Url = "Meal";
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
