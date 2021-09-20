using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;
using Web_API.Request;
using Web_API.Response;


namespace Web_API.Models
{
    public class Meal
    {
        DAL dl = new DAL();
        ClsSqlHelper objSqlHelper = new ClsSqlHelper();

        public ClsMealBookingResponse MealBooking(ClsMealBookingRequest clsMealBookingRequest)
        {
            DataSet ds = new DataSet();
            ClsMealBookingResponse objClsMealBookingResponse = new ClsMealBookingResponse();

            if (string.IsNullOrEmpty(clsMealBookingRequest.IMEI))
            {
                objClsMealBookingResponse.ErrorCode = "-1";
                objClsMealBookingResponse.ErrorMessage = "Invalid or Blank Device ID";
                return objClsMealBookingResponse;
            }
            string MastCode = clsMealBookingRequest.MastCode != null ? MastCode = clsMealBookingRequest.MastCode : MastCode = "0";
            object[] parameter = new object[]{
					MastCode,
                    clsMealBookingRequest.MealType          ,
                    clsMealBookingRequest.IMEI,
                    clsMealBookingRequest.EmpCode,
                    clsMealBookingRequest.ItemCode,
                    clsMealBookingRequest.BookingDate,
                    clsMealBookingRequest.Company,
                    clsMealBookingRequest.CanteenCode,
                    clsMealBookingRequest.IsActive,
                    clsMealBookingRequest.Quantity,
                    1
					};

            objClsMealBookingResponse.MastCode = dl.SP_ExecuteNonQueryRetVal(parameter, "OnSave_TblMealBooking").ToString();
            if (Convert.ToInt64(objClsMealBookingResponse.MastCode) >= 1)
            {

                objClsMealBookingResponse.ErrorCode = "0";
                objClsMealBookingResponse.ErrorMessage = "Success";
            }
            else
            {
                objClsMealBookingResponse.ErrorCode = "1";
                objClsMealBookingResponse.ErrorMessage = "Not Success";
            }


            return objClsMealBookingResponse;


        }

        public ClsSyncMealMasterResponse SyncMealMaster(ClsSyncMealMasterRequest clsSyncMealMasterRequest)
        {
            DataSet ds = new DataSet();
            ClsSyncMealMasterResponse objClsSyncMealMasterResponse = new ClsSyncMealMasterResponse();


            object[] parameter = new object[]{
               
					 clsSyncMealMasterRequest.SyncDate
                    ,clsSyncMealMasterRequest.MealMastCode
                    ,clsSyncMealMasterRequest.IMEI
                    ,clsSyncMealMasterRequest.UserCode
                    ,clsSyncMealMasterRequest.Company
                    ,clsSyncMealMasterRequest.CanteenCode
            };

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "GetAll_TblMealMaster_Device");


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objClsSyncMealMasterResponse.ErrorCode = Convert.ToString(ds.Tables[0].Rows[0]["ErrorCode"]);
                        objClsSyncMealMasterResponse.ErrorMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);

                        ClsSyncMealMasterRequestData[] ObjRes_List = new ClsSyncMealMasterRequestData[ds.Tables[0].Rows.Count];
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ClsSyncMealMasterRequestData objRes1 = new ClsSyncMealMasterRequestData();

                            objRes1.MastCode = ds.Tables[0].Rows[i]["MastCode"].ToString();
                            objRes1.MastName = ds.Tables[0].Rows[i]["MastName"].ToString();
                            objRes1.CodeType = ds.Tables[0].Rows[i]["CodeType"].ToString();
                            objRes1.StartTime = ds.Tables[0].Rows[i]["StartTime"].ToString();
                            objRes1.EndTime = ds.Tables[0].Rows[i]["EndTime"].ToString();
                            objRes1.IsActive = ds.Tables[0].Rows[i]["IsActive"].ToString();
                             objRes1.TokenNo = ds.Tables[0].Rows[i]["TokenNo"].ToString();

                            objRes1.CanteenCode = ds.Tables[0].Rows[i]["CanteenCode"].ToString();
                            // objRes1.Day = ds.Tables[0].Rows[i]["Day"].ToString();
                            ObjRes_List[i] = objRes1;
                        }
                        objClsSyncMealMasterResponse.SyncMealMasterData = ObjRes_List;
                    }
                    else
                    {
                        objClsSyncMealMasterResponse.SyncMealMasterData = null;
                    }
                }
                else
                {
                    objClsSyncMealMasterResponse.ErrorCode = "1003";
                    objClsSyncMealMasterResponse.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                }
            }
            else
            {
                objClsSyncMealMasterResponse.ErrorCode = "1003";
                objClsSyncMealMasterResponse.ErrorMessage = "DATASET NULL";
            }

            return objClsSyncMealMasterResponse;
        }

        public ClsGetMealBookingResponse GetMealBooking(ClsGetMealBookingRequest clsGetMealBookingRequest)
        {
            DataSet ds = new DataSet();
            ClsGetMealBookingResponse objClsGetMealBookingResponse = new ClsGetMealBookingResponse();


            object[] parameter = new object[]{
                    clsGetMealBookingRequest.SyncDate,
                    clsGetMealBookingRequest.IMEI,
                    clsGetMealBookingRequest.CanteenCode,
                    clsGetMealBookingRequest.MealBookCode,
                    clsGetMealBookingRequest.ItemCode
            };

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "Get_Mealbooking_For_Upload_CNT");


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {

                    objClsGetMealBookingResponse.ErrorCode = Convert.ToString(ds.Tables[0].Rows[0]["ErrorCode"]);
                    objClsGetMealBookingResponse.ErrorMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                    objClsGetMealBookingResponse.MastCode = Convert.ToString(ds.Tables[0].Rows[0]["MastCode"]);
                    objClsGetMealBookingResponse.EmpMastCode = Convert.ToString(ds.Tables[0].Rows[0]["EmpMastCode"]);
                    objClsGetMealBookingResponse.MealType = Convert.ToString(ds.Tables[0].Rows[0]["MealType"]);
                    objClsGetMealBookingResponse.ItemCode = Convert.ToString(ds.Tables[0].Rows[0]["ItemCode"]);
                    objClsGetMealBookingResponse.BookingDate = Convert.ToString(ds.Tables[0].Rows[0]["BookingDate"]);
                    objClsGetMealBookingResponse.CanteenCode = Convert.ToString(ds.Tables[0].Rows[0]["CanteenCode"]);
                    objClsGetMealBookingResponse.Quantity = Convert.ToString(ds.Tables[0].Rows[0]["Quantity"]);
            



                    //objClsGetMealBookingResponse.Emp_Id = Convert.ToString(ds.Tables[0].Rows[0]["Emp_Id"]);
                    //objClsGetMealBookingResponse.StaffName = Convert.ToString(ds.Tables[0].Rows[0]["StaffName"]);
                    //objClsGetMealBookingResponse.StaffPhoto = Convert.ToString(ds.Tables[0].Rows[0]["StaffPhoto"]);
                    //objClsGetMealBookingResponse.Validity = Convert.ToString(ds.Tables[0].Rows[0]["Validity"]);
                    //objClsGetMealBookingResponse.BIO1 = Convert.ToString(ds.Tables[0].Rows[0]["BIO1"]);
                    //objClsGetMealBookingResponse.BIO2 = Convert.ToString(ds.Tables[0].Rows[0]["BIO2"]);
                    //objClsGetMealBookingResponse.MatchingType = Convert.ToString(ds.Tables[0].Rows[0]["MatchingType"]);
                    //objClsGetMealBookingResponse.CardType = Convert.ToString(ds.Tables[0].Rows[0]["CardType"]);
                   
                    //objClsGetMealBookingResponse.Shift = Convert.ToString(ds.Tables[0].Rows[0]["Shift"]);
                    //objClsGetMealBookingResponse.Discount = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                    //objClsGetMealBookingResponse.Mobile = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
                    //objClsGetMealBookingResponse.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                    //objClsGetMealBookingResponse.AssignMeal = Convert.ToString(ds.Tables[0].Rows[0]["AssignMeal"]);
                    //objClsGetMealBookingResponse.EnrollID = Convert.ToString(ds.Tables[0].Rows[0]["EnrollID"]);
                    //objClsGetMealBookingResponse.ReferenceID = Convert.ToString(ds.Tables[0].Rows[0]["ReferenceID"]);
                    //objClsGetMealBookingResponse.EntityType = Convert.ToString(ds.Tables[0].Rows[0]["EntityType"]);
                    //objClsGetMealBookingResponse.Department = Convert.ToString(ds.Tables[0].Rows[0]["Department"]);

                }
                else
                {
                    objClsGetMealBookingResponse.ErrorCode = "1003";
                    objClsGetMealBookingResponse.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                }
            }
            else
            {
                objClsGetMealBookingResponse.ErrorCode = "1003";
                objClsGetMealBookingResponse.ErrorMessage = "DATASET NULL";
            }

            return objClsGetMealBookingResponse;
        }

        public ClsConfirmResponse OrderConfirm(ClsConfirmRequest clsConfirmRequest)
        {
            DataSet ds = new DataSet();
            ClsConfirmResponse objClsConfirmResponse = new ClsConfirmResponse();

            if (string.IsNullOrEmpty(clsConfirmRequest.IMEI))
            {
                objClsConfirmResponse.ErrorCode = "-1";
                objClsConfirmResponse.ErrorMessage = "Invalid or Blank Device ID";
                return objClsConfirmResponse;
            }
            string MastCode = clsConfirmRequest.MastCode != null ? MastCode = clsConfirmRequest.MastCode : MastCode = "0";
            object[] parameter = new object[]{
                clsConfirmRequest.MastCode,
					clsConfirmRequest.IMEI,
                    clsConfirmRequest.EmpCode,
                    clsConfirmRequest.EmpID,
                   clsConfirmRequest.BookingDate,
                   clsConfirmRequest.TokenNo ,
                   clsConfirmRequest.MealType, 
                    clsConfirmRequest.CanteenCode,
                    clsConfirmRequest.OrderID ,
                   clsConfirmRequest.ItemCode,
                  //  clsConfirmRequest.BookingDate,
                  //  clsConfirmRequest.Company,
                   // clsConfirmRequest.IsActive,
                    1
					};

            objClsConfirmResponse.MastCode = dl.SP_ExecuteNonQueryRetVal(parameter, "OnSave_TblBkpDmpTerminalDataCNT_CMS_New").ToString();
            if (Convert.ToInt64(objClsConfirmResponse.MastCode) >= 1)
            {
                //objClsMealBookingResponse.MastCode = objClsMealBookingResponse.ErrorCode;
                objClsConfirmResponse.ErrorCode = "0";
                objClsConfirmResponse.ErrorMessage = "Success";
            }
            else
            {
                objClsConfirmResponse.ErrorCode = "1";
                objClsConfirmResponse.ErrorMessage = "Not Success";
            }


            return objClsConfirmResponse;


        }
       

    }
}