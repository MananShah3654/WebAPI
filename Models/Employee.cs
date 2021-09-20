using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;

namespace Web_API
{
    public class Employee
    {
        ClsSqlHelper objSqlHelper = new ClsSqlHelper();
        DAL dl = new DAL();

        public ClsGetEmployeeDetailsResponse GetEmployeeDetails(ClsGetEmployeeDetailsRequest clsGetEmployeeDetailsRequest)
        {
            DataSet ds = new DataSet();
            ClsGetEmployeeDetailsResponse objClsGetEmployeeDetailsResponse = new ClsGetEmployeeDetailsResponse();

            if (string.IsNullOrEmpty(clsGetEmployeeDetailsRequest.DeviceSystemID.Trim()))
            {
                objClsGetEmployeeDetailsResponse.ErrorCode = "-1";
                objClsGetEmployeeDetailsResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsGetEmployeeDetailsResponse;
            }
            if (string.IsNullOrEmpty(clsGetEmployeeDetailsRequest.ReqID.Trim()))
            {
                objClsGetEmployeeDetailsResponse.ErrorCode = "-1";
                objClsGetEmployeeDetailsResponse.ErrorMessage = "Invalid Request ID.";
                return objClsGetEmployeeDetailsResponse;
            }

            object[] parameter = new object[]{
                 
                    clsGetEmployeeDetailsRequest.DeviceSystemID
                   ,clsGetEmployeeDetailsRequest.ReqID
             };

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "srv_MFSTAB_SP_GetEmployeeKioskDetails");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    objClsGetEmployeeDetailsResponse.Company = ds.Tables[0].Rows[0]["Company"].ToString();
                    objClsGetEmployeeDetailsResponse.CompanyLocation = ds.Tables[0].Rows[0]["CompanyLocation"].ToString();
                    objClsGetEmployeeDetailsResponse.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                    objClsGetEmployeeDetailsResponse.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                    objClsGetEmployeeDetailsResponse.EnrollID = ds.Tables[0].Rows[0]["EnrollID"].ToString();
                    objClsGetEmployeeDetailsResponse.EntityCategory = ds.Tables[0].Rows[0]["EntityCategory"].ToString();
                    objClsGetEmployeeDetailsResponse.EntityType = ds.Tables[0].Rows[0]["EntityType"].ToString();
                    objClsGetEmployeeDetailsResponse.KioskCode = ds.Tables[0].Rows[0]["KioskCode"].ToString();
                    objClsGetEmployeeDetailsResponse.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    objClsGetEmployeeDetailsResponse.ReferenceID = ds.Tables[0].Rows[0]["ReferenceID"].ToString();
                    if (ds.Tables[0].Rows[0]["CardValidity"] != DBNull.Value)
                    {
                        objClsGetEmployeeDetailsResponse.CardValidity = Convert.ToDateTime(ds.Tables[0].Rows[0]["CardValidity"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    objClsGetEmployeeDetailsResponse.ErrorCode = "0";
                    objClsGetEmployeeDetailsResponse.ErrorMessage = "SUCCESS";
                }

                else
                {
                    objClsGetEmployeeDetailsResponse.ErrorCode = "-1";
                    objClsGetEmployeeDetailsResponse.ErrorMessage = "No Data Found .!! Try again";
                }
            }
            else
            {
                objClsGetEmployeeDetailsResponse.ErrorCode = "-1";
                objClsGetEmployeeDetailsResponse.ErrorMessage = "No Data Found .!! Try again";
            }
            return objClsGetEmployeeDetailsResponse;
        }

        public ClsSaveEmployeeDetailsResponse SaveEmployeeDetails(ClsSaveEmployeeDetailsRequest clsSaveEmployeeDetailsRequest)
        {
            DataSet ds = new DataSet();
            ClsSaveEmployeeDetailsResponse objClsSaveEmployeeDetailsResponse = new ClsSaveEmployeeDetailsResponse();

            if (string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.DeviceSystemID.Trim()))
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsSaveEmployeeDetailsResponse;
            }
            if (string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.DeviceSystemID.Trim()))
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsSaveEmployeeDetailsResponse;
            }
            if (string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.KioskCode.Trim()))
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Kiosk Code.";
                return objClsSaveEmployeeDetailsResponse;
            }
            else
            {
                try
                {
                    Int64 temp = Convert.ToInt64(clsSaveEmployeeDetailsRequest.KioskCode);
                }
                catch (Exception ex)
                {
                    objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                    objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Kiosk Code.";
                    return objClsSaveEmployeeDetailsResponse;
                }
            }
            if (string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.Photo.Trim()))
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Photo.";
                return objClsSaveEmployeeDetailsResponse;
            }
            if (string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.CardNo.Trim()))
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Card No.";
                return objClsSaveEmployeeDetailsResponse;
            }
            if (string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.CardValidity.Trim()))
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Card Validity.";
                return objClsSaveEmployeeDetailsResponse;
            }
            else
            {
                try
                {
                    DateTime tmp = Convert.ToDateTime(Convert.ToDateTime(clsSaveEmployeeDetailsRequest.CardValidity).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                    objClsSaveEmployeeDetailsResponse.ErrorMessage = "Invalid Card Validity.";
                    return objClsSaveEmployeeDetailsResponse;
                }
            }

            int EnrolledFingerCount = 0;
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LT.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LI.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LM.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LR.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LL.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RT.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RI.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RM.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RR.Trim()))
            {
                EnrolledFingerCount++;
            }
            if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RL.Trim()))
            {
                EnrolledFingerCount++;
            }

            if (EnrolledFingerCount < 2)
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Minimum 2 fingers required.";
                return objClsSaveEmployeeDetailsResponse;
            }

            ClsEnrollmentDetails objToSave = new ClsEnrollmentDetails();
            ClsGlobal.GetEmployeeDetailsToSave(clsSaveEmployeeDetailsRequest, ref objToSave);

            object[] parameter = new object[]{
					    objToSave.DeviceSystemID,
                        objToSave.KioskCode,
                        objToSave.LT,
                        objToSave.LI,
                        objToSave.LM,
                        objToSave.LR,
                        objToSave.LL,
                        objToSave.RT,
                        objToSave.RI,
                        objToSave.RM,
                        objToSave.RR,
                        objToSave.RL,
                        objToSave.LT_ICS,
                        objToSave.LI_ICS,
                        objToSave.LM_ICS,
                        objToSave.LR_ICS,
                        objToSave.LL_ICS,
                        objToSave.RT_ICS,
                        objToSave.RI_ICS,
                        objToSave.RM_ICS,
                        objToSave.RR_ICS,
                        objToSave.RL_ICS,
                        objToSave.LT_ISO,
                        objToSave.LI_ISO,
                        objToSave.LM_ISO,
                        objToSave.LR_ISO,
                        objToSave.LL_ISO,
                        objToSave.RT_ISO,
                        objToSave.RI_ISO,
                        objToSave.RM_ISO,
                        objToSave.RR_ISO,
                        objToSave.RL_ISO,
                        objToSave.Photo,
                        objToSave.CardNo,
                        objToSave.CardValidity,
                        objToSave.MafisCode
                    
					};

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "srv_MFSTAB_SP_SaveEmployeeDetails");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {

                    objClsSaveEmployeeDetailsResponse.MastCode = ds.Tables[0].Rows[0]["MastCode"].ToString();
                    objClsSaveEmployeeDetailsResponse.EmpID = ds.Tables[0].Rows[0]["EmpID"].ToString();
                    objClsSaveEmployeeDetailsResponse.ErrorCode = "0";
                    objClsSaveEmployeeDetailsResponse.ErrorMessage = "SUCCESS";
                }
                else
                {
                    objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                    objClsSaveEmployeeDetailsResponse.ErrorMessage = "Something went wrong .!! Try again";
                }
            }
            else
            {
                objClsSaveEmployeeDetailsResponse.ErrorCode = "-1";
                objClsSaveEmployeeDetailsResponse.ErrorMessage = "Something went wrong .!! Try again";
            }
            return objClsSaveEmployeeDetailsResponse;
        }

    }
}