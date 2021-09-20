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
    public class Device
    {
        DAL dl = new DAL();
        public ClsSayHelloResponse SayHello(ClsSayHelloRequest clsSayHelloRequest)
        {
            DataSet ds = new DataSet();
            ClsSayHelloResponse objClsSayHelloResponse = new ClsSayHelloResponse();


            if (string.IsNullOrEmpty(clsSayHelloRequest.DeviceSystemID.Trim()))
            {
                objClsSayHelloResponse.ErrorCode = "-1";
                objClsSayHelloResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsSayHelloResponse;
            }
            if (string.IsNullOrEmpty(clsSayHelloRequest.DeviceDate.Trim()))
            {
                objClsSayHelloResponse.ErrorCode = "-1";
                objClsSayHelloResponse.ErrorMessage = "Invalid Device Date time.";
                return objClsSayHelloResponse;
            }
            else
            {
                try
                {
                    DateTime temp = Convert.ToDateTime(Convert.ToDateTime(clsSayHelloRequest.DeviceDate).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponse.ErrorCode = "-1";
                    objClsSayHelloResponse.ErrorMessage = "Invalid Device Date time.";
                    return objClsSayHelloResponse;
                }
            }
            if (string.IsNullOrEmpty(clsSayHelloRequest.FWVer.Trim()))
            {
                objClsSayHelloResponse.ErrorCode = "-1";
                objClsSayHelloResponse.ErrorMessage = "Invalid Device Firmware Version.";
                return objClsSayHelloResponse;
            }
            if (string.IsNullOrEmpty(clsSayHelloRequest.UserCount.Trim()))
            {
                objClsSayHelloResponse.ErrorCode = "-1";
                objClsSayHelloResponse.ErrorMessage = "Invalid User Count.";
                return objClsSayHelloResponse;
            }
            else
            {
                try
                {
                    Int64 tuc = Convert.ToInt64(clsSayHelloRequest.UserCount.Trim());
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponse.ErrorCode = "-1";
                    objClsSayHelloResponse.ErrorMessage = "Invalid User Count.";
                    return objClsSayHelloResponse;
                }
            }
            if (string.IsNullOrEmpty(clsSayHelloRequest.TransCount.Trim()))
            {
                objClsSayHelloResponse.ErrorCode = "-1";
                objClsSayHelloResponse.ErrorMessage = "Invalid Transection Count.";
                return objClsSayHelloResponse;
            }
            else
            {
                try
                {
                    Int64 tuc = Convert.ToInt64(clsSayHelloRequest.TransCount.Trim());
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponse.ErrorCode = "-1";
                    objClsSayHelloResponse.ErrorMessage = "Invalid Transection Count.";
                    return objClsSayHelloResponse;
                }
            }
            if (string.IsNullOrEmpty(clsSayHelloRequest.FPCount.Trim()))
            {
                objClsSayHelloResponse.ErrorCode = "-1";
                objClsSayHelloResponse.ErrorMessage = "Invalid FP Count.";
                return objClsSayHelloResponse;
            }
            else
            {
                try
                {
                    Int64 tuc = Convert.ToInt64(clsSayHelloRequest.FPCount.Trim());
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponse.ErrorCode = "-1";
                    objClsSayHelloResponse.ErrorMessage = "Invalid FP Count.";
                    return objClsSayHelloResponse;
                }
            }

            string[,] parameter = new string[,]{
				{"@DeviceSystemID",clsSayHelloRequest.DeviceSystemID},
				{"@DeviceDate",clsSayHelloRequest.DeviceDate},
				{"@FWVer",clsSayHelloRequest.FWVer},
				{"@UserCount",clsSayHelloRequest.UserCount},
				{"@TransCount",clsSayHelloRequest.TransCount},
                {"@FPCount",clsSayHelloRequest.FPCount}
                //{"@SyncDateTime",clsSayHelloRequest.SyncDateTime}
                //{"@Longitude",clsSayHelloRequest.Longitude},
                //{"@CanteenCode",clsSayHelloRequest.CanteenCode},
				};
            ds = dl.ExecuteStoredProcedureDS("srv_MFSTAB_SP_SayHello_DEMO", parameter);
            //ds = dl.ExecuteStoredProcedureDS("SP_Say_Hello_CNT_CMS", parameter);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        objClsSayHelloResponse.ErrorCode = "0";
                        objClsSayHelloResponse.ErrorMessage = "SUCCESS";
                        objClsSayHelloResponse.ServerDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ServerDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        objClsSayHelloResponse.CompanyCode = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        objClsSayHelloResponse.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                        objClsSayHelloResponse.CompanyLocationCode = ds.Tables[0].Rows[0]["CompanyLocationCode"].ToString();
                        objClsSayHelloResponse.CompanyLocationName = ds.Tables[0].Rows[0]["CompanyLocationName"].ToString();
                        objClsSayHelloResponse.GateCode = ds.Tables[0].Rows[0]["GateCode"].ToString();
                        objClsSayHelloResponse.GateName = ds.Tables[0].Rows[0]["GateName"].ToString();
                        objClsSayHelloResponse.DeviceMode = ds.Tables[0].Rows[0]["DeviceMode"].ToString();
                        objClsSayHelloResponse.DeviceApplicationType = ds.Tables[0].Rows[0]["DeviceApplicationType"].ToString();
                        objClsSayHelloResponse.DeviceApplicationTypeName = ds.Tables[0].Rows[0]["DeviceApplicationTypeName"].ToString();
                        objClsSayHelloResponse.DeviceActiveFlag = ds.Tables[0].Rows[0]["DeviceActiveFlag"].ToString();
                        objClsSayHelloResponse.PendingCommandFlag = ds.Tables[0].Rows[0]["PendingCommandFlag"].ToString();
                        objClsSayHelloResponse.AllowToEnrollFlag = ds.Tables[0].Rows[0]["AllowToEnrollFlag"].ToString();
                        objClsSayHelloResponse.SayHelloTime = ds.Tables[0].Rows[0]["SayHelloTime"].ToString();
                        objClsSayHelloResponse.AutoRestartTime = ds.Tables[0].Rows[0]["AutoRestartTime"].ToString();
                        objClsSayHelloResponse.DownloadUserTime = ds.Tables[0].Rows[0]["DownloadUserTime"].ToString();
                        objClsSayHelloResponse.UploadTransactionTime = ds.Tables[0].Rows[0]["UploadTransactionTime"].ToString();
                        objClsSayHelloResponse.DeviceCommandTime = ds.Tables[0].Rows[0]["DeviceCommandTime"].ToString();
                        objClsSayHelloResponse.RelayTime = ds.Tables[0].Rows[0]["RelayTime"].ToString();
                        objClsSayHelloResponse.DisplayTime = ds.Tables[0].Rows[0]["DisplayTime"].ToString();
                        objClsSayHelloResponse.CanteenCode = Convert.ToString(ds.Tables[0].Rows[0]["CanteenCode"]);
                        objClsSayHelloResponse.AppType = Convert.ToString(ds.Tables[0].Rows[0]["DeviceType"]);
                        objClsSayHelloResponse.newApplicationVersion = "1.14";
                        objClsSayHelloResponse.isForceUpdate = "1";
                        objClsSayHelloResponse.downloadUrl = "http://192.168.6.133:8031/APK/GSL-app-release_v1.14.apk";


                    }
                    else
                    {
                        objClsSayHelloResponse.ErrorCode = "1003";
                        objClsSayHelloResponse.ErrorMessage = "ROW COUNT NOT FOUND";
                    }

                    //if (ds.Tables[1].Rows.Count > 0)
                    //{

                    //    // objobjClsSayHelloResponse.CountEmployee = Convert.ToString(ds.Tables[1].Rows[0]["CountEmployee"]);
                    //    objClsSayHelloResponse.CountItem = Convert.ToString(ds.Tables[1].Rows[0]["CountItem"]);
                    //    objClsSayHelloResponse.CountMeal = Convert.ToString(ds.Tables[1].Rows[0]["CountMeal"]);
                    //    objClsSayHelloResponse.CountOrderBook = Convert.ToString(ds.Tables[1].Rows[0]["CountOrderBook"]);
                    //    objClsSayHelloResponse.CustMsg = Convert.ToString(ds.Tables[1].Rows[0]["CustMsg"]);

                    //}
                    //else
                    //{
                    //    // objobjClsSayHelloResponse.CountEmployee = "0";
                    //    objClsSayHelloResponse.CountItem = "0";
                    //    objClsSayHelloResponse.CountMeal = "0";
                    //    objClsSayHelloResponse.CountOrderBook = "0";
                    //    objClsSayHelloResponse.CustMsg = "";

                    //}
                }
                else
                {
                    objClsSayHelloResponse.ErrorCode = "1003";
                    objClsSayHelloResponse.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                }
            }
            else
            {
                objClsSayHelloResponse.ErrorCode = "1003";
                objClsSayHelloResponse.ErrorMessage = "DATASET NULL";
            }

            return objClsSayHelloResponse;


        }


        public ClsGetLicenseResponse GetLicense(ClsGetLicenseRequest clsGetLicenseRequest)
        {
            DataSet ds = new DataSet();
            ClsGetLicenseResponse objClsGetLicenseResponse = new ClsGetLicenseResponse();


            string[,] parameter = new string[,]{
				{"@DeviceId",clsGetLicenseRequest.DeviceId},
				
				};
            ds = dl.ExecuteStoredProcedureDS("srv_MFSTAB_SP_GetLicense", parameter);


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objClsGetLicenseResponse.ErrorCode = "0";
                        objClsGetLicenseResponse.ErrorMessage = "SUCCESS";
                        byte[] Licensebytes = (byte[])ds.Tables[0].Rows[0]["License"];
                        objClsGetLicenseResponse.License = Convert.ToBase64String(Licensebytes, 0, Licensebytes.Length).Trim();
                    }
                    else
                    {
                        objClsGetLicenseResponse.ErrorCode = "1003";
                        objClsGetLicenseResponse.ErrorMessage = "LICENSE NOT FOUND";
                    }
                }
                else
                {
                    objClsGetLicenseResponse.ErrorCode = "1003";
                    objClsGetLicenseResponse.ErrorMessage = "LICENSE NOT FOUND";
                }
            }
            else
            {
                objClsGetLicenseResponse.ErrorCode = "1003";
                objClsGetLicenseResponse.ErrorMessage = "LICENSE NOT FOUND";
            }
            return objClsGetLicenseResponse;
        }
    }
}