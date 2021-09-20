using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;

namespace Web_API
{
    public class Company
    {
        ClsSqlHelper objSqlHelper = new ClsSqlHelper();
        public ClsGetCompanyInfoResponse GetCompanyInfo(ClsGetCompanyInfoRequest clsGetCompanyInfoRequest)
        {
            DataSet ds = new DataSet();
            ClsGetCompanyInfoResponse objClsGetCompanyInfoResponse = new ClsGetCompanyInfoResponse();


            object[] parameter = new object[]{
                 
                    clsGetCompanyInfoRequest.CompanyCode
             };

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "srv_MFSTAB_SP_GetCompanyInfo");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    objClsGetCompanyInfoResponse.ServerDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ServerDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    objClsGetCompanyInfoResponse.CompanyCode = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                    objClsGetCompanyInfoResponse.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                    if (ds.Tables[0].Rows[0]["CompanyLogo"] == DBNull.Value)
                    {
                        objClsGetCompanyInfoResponse.CompanyLogo = "";
                    }
                    else
                    {
                        objClsGetCompanyInfoResponse.CompanyLogo = Convert.ToBase64String(((byte[])ds.Tables[0].Rows[0]["CompanyLogo"]));
                    }

                    objClsGetCompanyInfoResponse.ErrorCode = "0";
                    objClsGetCompanyInfoResponse.ErrorMessage = "SUCCESS";
                 }
                else
                {
                    objClsGetCompanyInfoResponse.ErrorCode = "1003";
                    objClsGetCompanyInfoResponse.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                }
            }
            else
            {
                objClsGetCompanyInfoResponse.ErrorCode = "1003";
                objClsGetCompanyInfoResponse.ErrorMessage = "DATASET NULL";
            }

            return objClsGetCompanyInfoResponse;
        }
    }
}