using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;
using Web_API.Response;
using Web_API.Request;


namespace Web_API
{
    public class Canteen
    {
        DAL dl = new DAL();
        public ClsSayHelloResponseCanteen SayHello(ClsSayHelloRequestCanteen ClsSayHelloRequestCanteen)
        {
            DataSet ds = new DataSet();
            ClsSayHelloResponseCanteen objClsSayHelloResponseCanteen = new ClsSayHelloResponseCanteen();


            if (string.IsNullOrEmpty(ClsSayHelloRequestCanteen.DeviceSystemID.Trim()))
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = "Invalid Device System ID.";
                return objClsSayHelloResponseCanteen;
            }
            if (string.IsNullOrEmpty(ClsSayHelloRequestCanteen.DeviceDate.Trim()))
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = "Invalid Device Date time.";
                return objClsSayHelloResponseCanteen;
            }
            else
            {
                try
                {
                    DateTime temp = Convert.ToDateTime(Convert.ToDateTime(ClsSayHelloRequestCanteen.DeviceDate).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponseCanteen.ErrorCode = "-1";
                    objClsSayHelloResponseCanteen.ErrorMessage = "Invalid Device Date time.";
                    return objClsSayHelloResponseCanteen;
                }
            }
            if (string.IsNullOrEmpty(ClsSayHelloRequestCanteen.FWVer.Trim()))
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = "Invalid Device Firmware Version.";
                return objClsSayHelloResponseCanteen;
            }
            if (string.IsNullOrEmpty(ClsSayHelloRequestCanteen.UserCount.Trim()))
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = "Invalid User Count.";
                return objClsSayHelloResponseCanteen;
            }
            else
            {
                try
                {
                    Int64 tuc = Convert.ToInt64(ClsSayHelloRequestCanteen.UserCount.Trim());
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponseCanteen.ErrorCode = "-1";
                    objClsSayHelloResponseCanteen.ErrorMessage = "Invalid User Count.";
                    return objClsSayHelloResponseCanteen;
                }
            }
            if (string.IsNullOrEmpty(ClsSayHelloRequestCanteen.TransCount.Trim()))
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = "Invalid Transection Count.";
                return objClsSayHelloResponseCanteen;
            }
            else
            {
                try
                {
                    Int64 tuc = Convert.ToInt64(ClsSayHelloRequestCanteen.TransCount.Trim());
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponseCanteen.ErrorCode = "-1";
                    objClsSayHelloResponseCanteen.ErrorMessage = "Invalid Transection Count.";
                    return objClsSayHelloResponseCanteen;
                }
            }
            if (string.IsNullOrEmpty(ClsSayHelloRequestCanteen.FPCount.Trim()))
            {
                objClsSayHelloResponseCanteen.ErrorCode = "-1";
                objClsSayHelloResponseCanteen.ErrorMessage = "Invalid FP Count.";
                return objClsSayHelloResponseCanteen;
            }
            else
            {
                try
                {
                    Int64 tuc = Convert.ToInt64(ClsSayHelloRequestCanteen.FPCount.Trim());
                }
                catch (Exception ex)
                {
                    objClsSayHelloResponseCanteen.ErrorCode = "-1";
                    objClsSayHelloResponseCanteen.ErrorMessage = "Invalid FP Count.";
                    return objClsSayHelloResponseCanteen;
                }
            }

            string[,] parameter = new string[,]{
				{"@DeviceSystemID",ClsSayHelloRequestCanteen.DeviceSystemID},
				{"@DeviceDate",ClsSayHelloRequestCanteen.DeviceDate},
				{"@FWVer",ClsSayHelloRequestCanteen.FWVer},
				{"@UserCount",ClsSayHelloRequestCanteen.UserCount},
				{"@TransCount",ClsSayHelloRequestCanteen.TransCount},
                {"@FPCount",ClsSayHelloRequestCanteen.FPCount},
                {"@SyncDateTime",ClsSayHelloRequestCanteen.SyncDateTime},
              

                //{"@Longitude",ClsSayHelloRequestCanteen.Longitude},
                //{"@CanteenCode",ClsSayHelloRequestCanteen.CanteenCode},
				};
            ds = dl.ExecuteStoredProcedureDS("srv_MFSTAB_SP_SayHello_Canteen", parameter);
            //ds = dl.ExecuteStoredProcedureDS("SP_Say_Hello_CNT_CMS", parameter);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        objClsSayHelloResponseCanteen.ErrorCode = "0";
                        objClsSayHelloResponseCanteen.ErrorMessage = "SUCCESS";
                        objClsSayHelloResponseCanteen.ServerDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ServerDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        objClsSayHelloResponseCanteen.CompanyCode = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        objClsSayHelloResponseCanteen.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                        objClsSayHelloResponseCanteen.CompanyLocationCode = ds.Tables[0].Rows[0]["CompanyLocationCode"].ToString();
                        objClsSayHelloResponseCanteen.CompanyLocationName = ds.Tables[0].Rows[0]["CompanyLocationName"].ToString();
                        objClsSayHelloResponseCanteen.GateCode = ds.Tables[0].Rows[0]["GateCode"].ToString();
                        objClsSayHelloResponseCanteen.GateName = ds.Tables[0].Rows[0]["GateName"].ToString();
                        objClsSayHelloResponseCanteen.DeviceMode = ds.Tables[0].Rows[0]["DeviceMode"].ToString();
                        objClsSayHelloResponseCanteen.DeviceApplicationType = ds.Tables[0].Rows[0]["DeviceApplicationType"].ToString();
                        objClsSayHelloResponseCanteen.DeviceApplicationTypeName = ds.Tables[0].Rows[0]["DeviceApplicationTypeName"].ToString();
                        objClsSayHelloResponseCanteen.DeviceActiveFlag = ds.Tables[0].Rows[0]["DeviceActiveFlag"].ToString();
                        objClsSayHelloResponseCanteen.PendingCommandFlag = ds.Tables[0].Rows[0]["PendingCommandFlag"].ToString();
                        objClsSayHelloResponseCanteen.AllowToEnrollFlag = ds.Tables[0].Rows[0]["AllowToEnrollFlag"].ToString();
                        objClsSayHelloResponseCanteen.SayHelloTime = ds.Tables[0].Rows[0]["SayHelloTime"].ToString();
                        objClsSayHelloResponseCanteen.AutoRestartTime = ds.Tables[0].Rows[0]["AutoRestartTime"].ToString();
                        objClsSayHelloResponseCanteen.DownloadUserTime = ds.Tables[0].Rows[0]["DownloadUserTime"].ToString();
                        objClsSayHelloResponseCanteen.UploadTransactionTime = ds.Tables[0].Rows[0]["UploadTransactionTime"].ToString();
                        objClsSayHelloResponseCanteen.DeviceCommandTime = ds.Tables[0].Rows[0]["DeviceCommandTime"].ToString();
                        objClsSayHelloResponseCanteen.RelayTime = ds.Tables[0].Rows[0]["RelayTime"].ToString();
                        objClsSayHelloResponseCanteen.DisplayTime = ds.Tables[0].Rows[0]["DisplayTime"].ToString();
                        objClsSayHelloResponseCanteen.CanteenCode = Convert.ToString(ds.Tables[0].Rows[0]["CanteenCode"]);
                        objClsSayHelloResponseCanteen.AppType = Convert.ToString(ds.Tables[0].Rows[0]["DeviceType"]);
                        objClsSayHelloResponseCanteen.DeviceSubType = Convert.ToInt32(ds.Tables[0].Rows[0]["DeviceSubType"]);



                    }
                    else
                    {
                        objClsSayHelloResponseCanteen.ErrorCode = "1003";
                        objClsSayHelloResponseCanteen.ErrorMessage = "ROW COUNT NOT FOUND";
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        // objobjClsSayHelloResponseCanteen.CountEmployee = Convert.ToString(ds.Tables[1].Rows[0]["CountEmployee"]);
                        objClsSayHelloResponseCanteen.CountItem = Convert.ToString(ds.Tables[1].Rows[0]["CountItem"]);
                        objClsSayHelloResponseCanteen.CountMeal = Convert.ToString(ds.Tables[1].Rows[0]["CountMeal"]);
                        objClsSayHelloResponseCanteen.CountOrderBook = Convert.ToString(ds.Tables[1].Rows[0]["CountOrderBook"]);
                        objClsSayHelloResponseCanteen.CustMsg = Convert.ToString(ds.Tables[1].Rows[0]["CustMsg"]);

                    }
                    else
                    {
                        // objobjClsSayHelloResponseCanteen.CountEmployee = "0";
                        objClsSayHelloResponseCanteen.CountItem = "0";
                        objClsSayHelloResponseCanteen.CountMeal = "0";
                        objClsSayHelloResponseCanteen.CountOrderBook = "0";
                        objClsSayHelloResponseCanteen.CustMsg = "";

                    }
                }
                else
                {
                    objClsSayHelloResponseCanteen.ErrorCode = "1003";
                    objClsSayHelloResponseCanteen.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                }
            }
            else
            {
                objClsSayHelloResponseCanteen.ErrorCode = "1003";
                objClsSayHelloResponseCanteen.ErrorMessage = "DATASET NULL";
            }

            return objClsSayHelloResponseCanteen;


        }

    }
}