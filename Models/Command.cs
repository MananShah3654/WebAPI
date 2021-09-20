using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;

namespace Web_API
{
    public class Command
    {
        ClsSqlHelper objSqlHelper = new ClsSqlHelper();

        public ClsGetPendingCommandResponse GetPendingCommand(ClsGetPendingCommandRequest clsGetPendingCommandRequest)
        {
            DataSet ds = new DataSet();
            ClsGetPendingCommandResponse objClsGetPendingCommandResponse = new ClsGetPendingCommandResponse();

            if (string.IsNullOrEmpty(clsGetPendingCommandRequest.DeviceSystemID.Trim()))
            {
                objClsGetPendingCommandResponse.ErrorCode = "-1";
                objClsGetPendingCommandResponse.ErrorMessage = "Invalid Device System ID.";
                return objClsGetPendingCommandResponse;
            }
            if (string.IsNullOrEmpty(clsGetPendingCommandRequest.LastProcessedCommandCode.Trim()))
            {
                objClsGetPendingCommandResponse.ErrorCode = "-1";
                objClsGetPendingCommandResponse.ErrorMessage = "Invalid Last Processed Command Code.";
                return objClsGetPendingCommandResponse;
            }

            object[] parameter = new object[]{
                 
                    clsGetPendingCommandRequest.DeviceSystemID
                   ,clsGetPendingCommandRequest.LastProcessedCommandCode
             };

            ds = objSqlHelper.GetDataSetAddWithValue(parameter, "srv_MFSTAB_SP_GetPendingCommand");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    objClsGetPendingCommandResponse.MastCode = ds.Tables[0].Rows[0]["MastCode"].ToString();
                    objClsGetPendingCommandResponse.CommandCode = ds.Tables[0].Rows[0]["Command"].ToString();
                    objClsGetPendingCommandResponse.CommandDesc = ds.Tables[0].Rows[0]["CommandDesc"].ToString();
                    objClsGetPendingCommandResponse.ErrorCode = "0";
                    objClsGetPendingCommandResponse.ErrorMessage = "SUCCESS";
                }

                else
                {
                    objClsGetPendingCommandResponse.ErrorCode = "-1";
                    objClsGetPendingCommandResponse.ErrorMessage = "No Data Found .!! Try again";
                }
            }
            else
            {
                objClsGetPendingCommandResponse.ErrorCode = "-1";
                objClsGetPendingCommandResponse.ErrorMessage = "No Data Found .!! Try again";
            }
            return objClsGetPendingCommandResponse;
        }
    }
}