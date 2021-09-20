using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;

namespace Web_API
{
    public class UserDownload
    {
        ClsSqlHelper objSqlHelper = new ClsSqlHelper();

        public ClsGetPendingUserDownloadResponse GetPendingUserDownload(ClsGetPendingUserDownloadRequest clsGetPendingUserDownloadRequest)
        {
            DataSet ds = new DataSet();
            ClsGetPendingUserDownloadResponse objClsGetPendingUserDownloadResponse = new ClsGetPendingUserDownloadResponse();

            if (string.IsNullOrEmpty(clsGetPendingUserDownloadRequest.DeviceSystemID.Trim()))
            {
                objClsGetPendingUserDownloadResponse.ErrorCode = "-1";
                objClsGetPendingUserDownloadResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsGetPendingUserDownloadResponse;
            }
            if (string.IsNullOrEmpty(clsGetPendingUserDownloadRequest.LastEmpCode.Trim()))
            {
                objClsGetPendingUserDownloadResponse.ErrorCode = "-1";
                objClsGetPendingUserDownloadResponse.ErrorMessage = "Invalid Last Employee Code.";
                return objClsGetPendingUserDownloadResponse;
            }

            object[] parameter = new object[]{
                 
                    clsGetPendingUserDownloadRequest.DeviceSystemID
                   ,clsGetPendingUserDownloadRequest.LastEmpCode
             };

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "srv_MFSTAB_SP_GetPendingUserToDownload");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    objClsGetPendingUserDownloadResponse.Name      = ds.Tables[0].Rows[0]["Name"].ToString();
                    objClsGetPendingUserDownloadResponse.EmpCode   = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                    objClsGetPendingUserDownloadResponse.EmpID     = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    objClsGetPendingUserDownloadResponse.CardNo    = ds.Tables[0].Rows[0]["CardNo"].ToString();
                    objClsGetPendingUserDownloadResponse.ValidFrom = ds.Tables[0].Rows[0]["ValidFrom"].ToString();
                    objClsGetPendingUserDownloadResponse.ValidTo   = ds.Tables[0].Rows[0]["ValidTo"].ToString();
                    objClsGetPendingUserDownloadResponse.IsBlock   = ds.Tables[0].Rows[0]["IsBlock"].ToString();
                    objClsGetPendingUserDownloadResponse.IsDeleted = ds.Tables[0].Rows[0]["IsDeleted"].ToString();
                    objClsGetPendingUserDownloadResponse.CardType  = ds.Tables[0].Rows[0]["CardType"].ToString();
                    objClsGetPendingUserDownloadResponse.EnrollID  = ds.Tables[0].Rows[0]["EnrollID"].ToString();

                    if (ds.Tables[0].Rows[0]["LT"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.LT = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["LT"]);
                    }
                    if (ds.Tables[0].Rows[0]["LI"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.LI = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["LI"]);
                    }
                    if (ds.Tables[0].Rows[0]["LM"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.LM = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["LM"]);
                    }
                    if (ds.Tables[0].Rows[0]["LR"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.LR = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["LR"]);
                    }
                    if (ds.Tables[0].Rows[0]["LL"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.LL = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["LL"]);
                    }
                    if (ds.Tables[0].Rows[0]["RT"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.RT = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["RT"]);
                    }
                    if (ds.Tables[0].Rows[0]["RI"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.RI = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["RI"]);
                    }
                    if (ds.Tables[0].Rows[0]["RM"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.RM = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["RM"]);
                    }
                    if (ds.Tables[0].Rows[0]["RR"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.RR = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["RR"]);
                    }
                    if (ds.Tables[0].Rows[0]["RL"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.RL = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["RL"]);
                    }
                    if (ds.Tables[0].Rows[0]["Photo"] != DBNull.Value)
                    {
                        objClsGetPendingUserDownloadResponse.Photo = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["Photo"]);
                    }
                    objClsGetPendingUserDownloadResponse.ErrorCode = "0";
                    objClsGetPendingUserDownloadResponse.ErrorMessage = "SUCCESS";
                }

                else
                {
                    objClsGetPendingUserDownloadResponse.ErrorCode = "-1";
                    objClsGetPendingUserDownloadResponse.ErrorMessage = "No Data Found .!! Try again";
                }
            }
            else
            {
                objClsGetPendingUserDownloadResponse.ErrorCode = "-1";
                objClsGetPendingUserDownloadResponse.ErrorMessage = "No Data Found .!! Try again";
            }
            return objClsGetPendingUserDownloadResponse;
        }
    }
}