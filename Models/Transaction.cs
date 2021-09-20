using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;
using AttendanceProcess;

namespace Web_API
{
    public class Transaction
    {
        DAL dl = new DAL();
        public ClsTransactionResponse SaveSuccess(ClsSaveSuccessRequest clsSaveSuccessRequest)
        {
            DataSet ds = new DataSet();
            byte[] Photo = null;
            ClsTransactionResponse objClsTransactionResponse = new ClsTransactionResponse();
            //MANAN

            if (string.IsNullOrEmpty(clsSaveSuccessRequest.DeviceSystemID.Trim()))
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsTransactionResponse;
            }
            if (string.IsNullOrEmpty(clsSaveSuccessRequest.EmpID.Trim()))
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Invalid Employee ID.";
                return objClsTransactionResponse;
            }
            if (string.IsNullOrEmpty(clsSaveSuccessRequest.InOutTime.Trim()))
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Invalid Punch timestamp.";
                return objClsTransactionResponse;
            }

            else
            {
                try
                {
                    DateTime temp = Convert.ToDateTime(Convert.ToDateTime(clsSaveSuccessRequest.InOutTime).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsTransactionResponse.ErrorCode = "-1";
                    objClsTransactionResponse.ErrorMessage = "Invalid Punch Time Stamp.";
                    return objClsTransactionResponse;
                }
            }
            //try
            //{
            //    if (string.IsNullOrEmpty(clsSaveSuccessRequest.Photo.Trim()))
            //    {
            //        objClsTransactionResponse.ErrorCode = "-1";
            //        objClsTransactionResponse.ErrorMessage = "Photo cannot be null.";
            //        return objClsTransactionResponse;
                    
            //    }
            //    else
            //    {
            //        Photo = Convert.FromBase64String(clsSaveSuccessRequest.Photo);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteErrorLog("ErrorMessage Photo - " + ex.Message);
            //}
            


            //clsSaveSuccessRequest.PhotoImage = Convert.FromBase64String(clsSaveSuccessRequest.Photo);

            object[] parameter = new object[]{
					clsSaveSuccessRequest.DeviceSystemID,
                    clsSaveSuccessRequest.EmpID   ,
                    clsSaveSuccessRequest.InOutTime,
                    //clsSaveSuccessRequest.PhotoImage
                    Photo
					};

            objClsTransactionResponse.ErrorCode = dl.SP_ExecuteNonQueryRetVal(parameter, "srv_MFSTAB_SP_SaveSuccessTransaction").ToString();
            LogManager.WriteErrorLog("ErrorCode - " + objClsTransactionResponse.ErrorCode.ToString());
            if (Convert.ToInt64(objClsTransactionResponse.ErrorCode) >= 1)
            {
                objClsTransactionResponse.ErrorCode = "0";
                objClsTransactionResponse.ErrorMessage = "Success";

                try
                {
                    // Real time attendace process
                    LogManager.WriteErrorLog("Attendance proces start - " + clsSaveSuccessRequest.EmpID.ToString());
                    AttendanceCls Atd = new AttendanceCls();
                    Boolean b;
                    b = false;
                    DateTime FromDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"));
                    DateTime EndDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    LogManager.WriteErrorLog("Attendance process - " + clsSaveSuccessRequest.EmpID.ToString());
                    LogManager.WriteErrorLog("Attendance process From Date , End Date- " + FromDate + "," + EndDate);
                    b = Atd.btnProcesswithByID(clsSaveSuccessRequest.EmpID, clsSaveSuccessRequest.DeviceSystemID, FromDate, EndDate);
                    //Atd.btnProcesswithByID(clsSaveSuccessRequest.EmpID, clsSaveSuccessRequest.DeviceSystemID, Convert.ToDateTime(clsSaveSuccessRequest.InOutTime).ToString("yyyy-MM-dd"));
                    LogManager.WriteErrorLog("Attendance process END ");
                }
                catch (Exception ex)
                {
                    LogManager.WriteErrorLog("Attendance eRROR - " + ex.Message);
                }

            }
            else
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Not Success";
            }

            return objClsTransactionResponse;

        }

        public ClsTransactionResponse SaveReject(ClsSaveRejectRequest clsSaveRejectRequest)
        {
            DataSet ds = new DataSet();
            ClsTransactionResponse objClsTransactionResponse = new ClsTransactionResponse();
            byte[] Photo = null;

            if (string.IsNullOrEmpty(clsSaveRejectRequest.DeviceSystemID.Trim()))
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsTransactionResponse;
            }

            if (string.IsNullOrEmpty(clsSaveRejectRequest.PunchDateTime.Trim()))
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Invalid Punch time Stamp.";
                return objClsTransactionResponse;
            }
            else
            {
                try
                {
                    DateTime temp = Convert.ToDateTime(Convert.ToDateTime(clsSaveRejectRequest.PunchDateTime).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsTransactionResponse.ErrorCode = "-1";
                    objClsTransactionResponse.ErrorMessage = "Invalid Punch Time Stamp.";
                    return objClsTransactionResponse;
                }
            }
            if (string.IsNullOrEmpty(clsSaveRejectRequest.RejectType.Trim()))
            {
                objClsTransactionResponse.ErrorCode = "-1";
                objClsTransactionResponse.ErrorMessage = "Invalid Reject Type.";
                return objClsTransactionResponse;
            }
            else
            {
                try
                {
                    Int64 temprt = Convert.ToInt64(clsSaveRejectRequest.RejectType.Trim());
                }
                catch (Exception)
                {
                    objClsTransactionResponse.ErrorCode = "-1";
                    objClsTransactionResponse.ErrorMessage = "Invalid Reject Type.";
                    return objClsTransactionResponse;
                }
            }
            try
            {
                //if (string.IsNullOrEmpty(clsSaveRejectRequest.Photo.Trim()))
                //{
                //    objClsTransactionResponse.ErrorCode = "-1";
                //    objClsTransactionResponse.ErrorMessage = "Photo cannot be null.";
                //    return objClsTransactionResponse;

                //}
                //else
                //{
                    Photo = Convert.FromBase64String(clsSaveRejectRequest.Photo);
                //}
            }
            catch (Exception ex)
            {
                LogManager.WriteErrorLog("ErrorMessage Photo - " + ex.Message);
            }
            

            object[] parameter = new object[]{
					clsSaveRejectRequest.DeviceSystemID
                    ,clsSaveRejectRequest.EmpID   
                    ,clsSaveRejectRequest.CardNo
                    ,clsSaveRejectRequest.RejectType
                    ,clsSaveRejectRequest.CardType
                    ,clsSaveRejectRequest.PunchDateTime
                    ,clsSaveRejectRequest.OfflineID
                    ,Photo
					};

            objClsTransactionResponse.ErrorCode = dl.SP_ExecuteNonQueryRetVal(parameter, "srv_MFSTAB_SaveRejectTransaction").ToString();
            if (Convert.ToInt64(objClsTransactionResponse.ErrorCode) >= 1)
            {
                objClsTransactionResponse.ErrorCode = "0";
                objClsTransactionResponse.ErrorMessage = "Success";
            }
            else
            {
                objClsTransactionResponse.ErrorCode = "0";
                objClsTransactionResponse.ErrorMessage = "Unhandeled Exception Occured .!! Try again";
            }

            return objClsTransactionResponse;

        }

    }
}