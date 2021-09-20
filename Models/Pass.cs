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
    public class Pass
    {
        DAL dl = new DAL();
        public ClsSyncPassResponse GetPassRequest(ClsSyncPassRequest ClsPassRequest)
        {
            DataSet ds = new DataSet();
            ClsSyncPassResponse objClsSyncPassResponse = new ClsSyncPassResponse();


            if (string.IsNullOrEmpty(ClsPassRequest.DeviceSystemID.Trim()))
            {
                objClsSyncPassResponse.ErrorCode = "-1";
                objClsSyncPassResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsSyncPassResponse;
            }
            if (string.IsNullOrEmpty(ClsPassRequest.DeviceDate.Trim()))
            {
                objClsSyncPassResponse.ErrorCode = "-1";
                objClsSyncPassResponse.ErrorMessage = "Invalid Device Date time.";
                return objClsSyncPassResponse;
            }
            else
            {
                try
                {
                    DateTime temp = Convert.ToDateTime(Convert.ToDateTime(ClsPassRequest.DeviceDate).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsSyncPassResponse.ErrorCode = "-1";
                    objClsSyncPassResponse.ErrorMessage = "Invalid Device Date time.";
                    return objClsSyncPassResponse;
                }
            }
            if (string.IsNullOrEmpty(ClsPassRequest.LastSyncDateTime.Trim()))
            {
                objClsSyncPassResponse.ErrorCode = "-1";
                objClsSyncPassResponse.ErrorMessage = "Invalid Sync Date time.";
                return objClsSyncPassResponse;
            }
            else
            {
                try
                {
                    DateTime temp = Convert.ToDateTime(Convert.ToDateTime(ClsPassRequest.LastSyncDateTime).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsSyncPassResponse.ErrorCode = "-1";
                    objClsSyncPassResponse.ErrorMessage = "Invalid Sync Date time.";
                    return objClsSyncPassResponse;
                }
            }
            if (string.IsNullOrEmpty(ClsPassRequest.Flag.Trim()))
            {
                objClsSyncPassResponse.ErrorCode = "-1";
                objClsSyncPassResponse.ErrorMessage = "Invalid Flag.";
                return objClsSyncPassResponse;
            }


            string[,] parameter = new string[,]{
				
                {"@DeviceSystemID",ClsPassRequest.DeviceSystemID},
				{"@DeviceDate",ClsPassRequest.DeviceDate},
				{"@LastSyncDateTime",ClsPassRequest.LastSyncDateTime},
                {"@Flag",ClsPassRequest.Flag},// Flag -1 Bhatta Pass , 2 - Gate Pass
				
				};
            ds = dl.ExecuteStoredProcedureDS("GetPassRequest_WebApi", parameter);

             if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            objClsSyncPassResponse.ErrorCode = "0";
                            objClsSyncPassResponse.ErrorMessage = "SUCCESS";
                            objClsSyncPassResponse.GatePassTime = "15:00";

                            ClsSyncPassData[] ObjRes_List = new ClsSyncPassData[ds.Tables[0].Rows.Count];
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ClsSyncPassData objRes1 = new ClsSyncPassData();

                                objRes1.EnrollID = ds.Tables[0].Rows[i]["EnrollID"].ToString();
                                objRes1.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                                objRes1.PermittedEnrollID = ds.Tables[0].Rows[i]["PermittedEnrollID"].ToString();
                                objRes1.PermittedEmployeeName = ds.Tables[0].Rows[i]["PermittedEmployeeName"].ToString();
                                objRes1.PassID = ds.Tables[0].Rows[i]["PassID"].ToString();
                                objRes1.PassCode = ds.Tables[0].Rows[i]["PassCode"].ToString();
                                objRes1.GatePassType = ds.Tables[0].Rows[i]["GatePassType"].ToString();
                                objRes1.Date = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString()).ToString("yyyy-MM-dd");
                                objRes1.StartTime = ds.Tables[0].Rows[i]["StartTime"].ToString();
                                objRes1.EndTime = ds.Tables[0].Rows[i]["EndTime"].ToString();

                                ObjRes_List[i] = objRes1;

                            }

                            objClsSyncPassResponse.PassData = ObjRes_List;

                        }
                        else
                        {
                            objClsSyncPassResponse.PassData = null;
                        }
                    }
                    else
                    {
                        objClsSyncPassResponse.ErrorCode = "1003";
                        objClsSyncPassResponse.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                    }
                }
           
            else
            {
                objClsSyncPassResponse.ErrorCode = "1003";
                objClsSyncPassResponse.ErrorMessage = "DATASET NULL";
            }

            return objClsSyncPassResponse;


        }
        public ClsPassTransactionResponse PassTransaction(ClsPassTransactionRequest clsPassTransactionRequest)
        {
            DataSet ds = new DataSet();
            ClsPassTransactionResponse objClsPassTransactionResponse = new ClsPassTransactionResponse();

            if (string.IsNullOrEmpty(clsPassTransactionRequest.IMEI))
            {
                objClsPassTransactionResponse.ErrorCode = "-1";
                objClsPassTransactionResponse.ErrorMessage = "Invalid or Blank Device ID";
                return objClsPassTransactionResponse;
            }
            
            object[] parameter = new object[]{
					
                    clsPassTransactionRequest.IMEI,
                    clsPassTransactionRequest.EnrollID,
                    clsPassTransactionRequest.EmpID,
                    clsPassTransactionRequest.In_out_time,
                    clsPassTransactionRequest.GatePassType,
                    clsPassTransactionRequest.PassCode,
                    clsPassTransactionRequest.Flag,
                    clsPassTransactionRequest.AuthType,
                    clsPassTransactionRequest.RejectType,
					};
         

            objClsPassTransactionResponse.MastCode = dl.SP_ExecuteNonQueryRetVal(parameter, "OnSave_TblBkpDmpTerminalData_Pass").ToString();
            if (Convert.ToInt64(objClsPassTransactionResponse.MastCode) >= 1)
            {
                if( clsPassTransactionRequest.RejectType == "0")
                {
                    objClsPassTransactionResponse.ErrorCode = "0";
                    objClsPassTransactionResponse.ErrorMessage = "Success";
                }
                else if (clsPassTransactionRequest.RejectType == "1")
                {
                    objClsPassTransactionResponse.ErrorCode = "1";
                    objClsPassTransactionResponse.ErrorMessage = "Rejected";
                }
                else if (clsPassTransactionRequest.RejectType == "2")
                {
                    objClsPassTransactionResponse.ErrorCode = "1";
                    objClsPassTransactionResponse.ErrorMessage = "Rejected";
                }

                
            }
            else
            {
                objClsPassTransactionResponse.ErrorCode = "1";
                objClsPassTransactionResponse.ErrorMessage = "Not Success";
            }


            return objClsPassTransactionResponse;


        }

    }
}