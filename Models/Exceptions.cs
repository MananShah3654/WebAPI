using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;

namespace Web_API.Models
{
    public class Exceptions
    {
        #region Objects
        DAL objDAL = new DAL();
#endregion

        #region Property


        public string Page { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public long UserCode { get; set; }
        public string UserName { get; set; }


        #endregion

        #region User Defined Function

        public bool Save()
        {
            try
            {
                var objParameters = new object[]
                {
                    Page,
                    Url,
                    Message,
                    StackTrace,
                    UserCode
                    
                };
                string[,] parameter = new string[,]{
				{"@Page",Page},
				{"@Url",Url},
				{"@Message",Message},
				{"@StackTrace",StackTrace},
				{"@UserCode",Convert.ToString(UserCode)}};

                //DataSet ds = _objClsExceptionDal.SaveDal(objParameters);
                //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //    //TODO Implement Threading for Sending Email 
                //    SendEmail(Convert.ToString(ds.Tables[0].Rows[0]["ErrorID"]));
                DataSet ds = objDAL.ExecuteStoredProcedureDS("OnSave_SP_TblErrorLog", parameter);

                return true;
            }
            catch { 
                //TODO
                return false;
            }           
            
        
        }
        #endregion
    }
}