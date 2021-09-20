using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public class ClsGetPendingUserDownloadResponse 
    {
        public string Name { get; set; }
        public string EmpCode { get; set; }
        public string EmpID { get; set; }
        public string CardNo { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string IsBlock { get; set; }
        public string IsDeleted { get; set; }
        public string CardType { get; set; }
        public string LT { get; set; }
        public string LI { get; set; }
        public string LM { get; set; }
        public string LR { get; set; }
        public string LL { get; set; }
        public string RT { get; set; }
        public string RI { get; set; }
        public string RM { get; set; }
        public string RR { get; set; }
        public string RL { get; set; }
        public string Photo { get; set; }
        public string EnrollID { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMessage{ get; set; }
    }
}