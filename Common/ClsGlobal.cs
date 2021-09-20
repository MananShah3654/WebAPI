using Innovatrics.AnsiIso;
using Innovatrics.IEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API
{
    public static class ClsGlobal
    {
        #region"IDKIT Declaration"
        public static IDKit idkit;

        public static byte[] universalLicense = new byte[] {
    0x49,
    0x43,
    0x5f,
    0x4c,
    3,
    0,
    0x44,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0x49,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x30,
    0x31,
    0,
    0,
    0,
    0,
    0x49,
    0x44,
    0x44,
    0x65,
    0x6d,
    0x6f,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    1,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0x70,
    0x2f,
    0xc1,
    0x35,
    0x79,
    0xe2,
    0xc3,
    0xf4,
    0x24,
    0x40,
    30,
    0x7c,
    0x79,
    0x7d,
    0xc2,
    0x5c,
    0x7d,
    0x2b,
    0xee,
    0x2e,
    0xa3,
    160,
    0xe1,
    0x71,
    0xee,
    0x33,
    0x8f,
    0xae,
    0x9c,
    0x8f,
    190,
    8,
    0x30,
    0xec,
    0x5e,
    0xe8,
    0x35,
    0x83,
    3,
    1,
    0x7e,
    10,
    0xc2,
    0x3d,
    0x4d,
    0x23,
    0x15,
    90,
    0x69,
    0x9d,
    0xd0,
    0x9c,
    0xb0,
    0x2e,
    60,
    0x97,
    0x75,
    0xb2,
    220,
    0x48,
    0xdb,
    0x71,
    0xe7,
    180,
    0xe8,
    0xb1,
    230,
    0x99,
    0xf9,
    0xd4,
    0x22,
    0xcb,
    0xc4,
    0x77,
    0xd7,
    0x67,
    0xbb,
    0x38,
    0x23,
    0x55,
    0x4a,
    0x73,
    0xc1,
    0x6a,
    0xec,
    0xaf,
    0xfe,
    0x66,
    0xfc,
    0x38,
    0xd1,
    0xb9,
    0xcd,
    6,
    0x39,
    0xc2,
    0x37,
    0x95,
    0x5c,
    0xe0,
    0xb8,
    0x22,
    0xbc,
    0xd3,
    0x71,
    4,
    0x73,
    0x47,
    0x43,
    5,
    0x61,
    0x11,
    0x9a,
    0x37,
    240,
    0x67,
    0xd5,
    0xa4,
    0xc6,
    0x5f,
    0xea,
    4,
    0xb6,
    190,
    0x1c,
    0x7b,
    8,
    0x3e
};
        #endregion

        public static byte[] ExtractISO(byte[] FingerImage)
        {
            try
            {
                if (FingerImage == null)
                {
                    return null;
                }

                idkit = IDKit.GetInstance(universalLicense);
                IEngine.SetLicenseContent(universalLicense, 196);
                IEngine.Init();
                User iUser = new User();
                Fingerprint fpLeft = new Fingerprint(FingerImage, Innovatrics.IEngine.FingerPosition.LeftIndex);
                iUser.Add(fpLeft);
                return iUser.ExportTemplate(Innovatrics.IEngine.TemplateFormat.ISO);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static byte[] ExtractICS(byte[] FingerImage)
        {
            try
            {
                if (FingerImage == null)
                {
                    return null;
                }

                idkit = IDKit.GetInstance(universalLicense);
                IEngine.SetLicenseContent(universalLicense, 196);
                IEngine.Init();
                User iUser = new User();
                Fingerprint fpLeft = new Fingerprint(FingerImage, Innovatrics.IEngine.FingerPosition.LeftIndex);
                iUser.Add(fpLeft);
                return iUser.ExportTemplate(Innovatrics.IEngine.TemplateFormat.ICS);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void GetEmployeeDetailsToSave(ClsSaveEmployeeDetailsRequest clsSaveEmployeeDetailsRequest, ref ClsEnrollmentDetails objClsEnrollmentDetails)
        {
            try
            {
                objClsEnrollmentDetails.CardNo = clsSaveEmployeeDetailsRequest.CardNo;
                objClsEnrollmentDetails.CardValidity = clsSaveEmployeeDetailsRequest.CardValidity;
                objClsEnrollmentDetails.DeviceSystemID = clsSaveEmployeeDetailsRequest.DeviceSystemID;
                objClsEnrollmentDetails.KioskCode = clsSaveEmployeeDetailsRequest.KioskCode;
                objClsEnrollmentDetails.Photo = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.Photo);
                objClsEnrollmentDetails.MafisCode = "0";
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LT.Trim()))
                {
                    objClsEnrollmentDetails.LT = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LT);
                    objClsEnrollmentDetails.LT_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LT));
                    objClsEnrollmentDetails.LT_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LT));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LI.Trim()))
                {
                    objClsEnrollmentDetails.LI = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LI);
                    objClsEnrollmentDetails.LI_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LI));
                    objClsEnrollmentDetails.LI_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LI));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LM.Trim()))
                {
                    objClsEnrollmentDetails.LM = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LM);
                    objClsEnrollmentDetails.LM_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LM));
                    objClsEnrollmentDetails.LM_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LM));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LR.Trim()))
                {
                    objClsEnrollmentDetails.LR = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LR);
                    objClsEnrollmentDetails.LR_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LR));
                    objClsEnrollmentDetails.LR_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LR));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.LL.Trim()))
                {
                    objClsEnrollmentDetails.LL = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LL);
                    objClsEnrollmentDetails.LL_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LL));
                    objClsEnrollmentDetails.LL_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.LL));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RT.Trim()))
                {
                    objClsEnrollmentDetails.RT = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RT);
                    objClsEnrollmentDetails.RT_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RT));
                    objClsEnrollmentDetails.RT_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RT));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RI.Trim()))
                {
                    objClsEnrollmentDetails.RI = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RI);
                    objClsEnrollmentDetails.RI_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RI));
                    objClsEnrollmentDetails.RI_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RI));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RM.Trim()))
                {
                    objClsEnrollmentDetails.RM = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RM);
                    objClsEnrollmentDetails.RM_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RM));
                    objClsEnrollmentDetails.RM_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RM));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RR.Trim()))
                {
                    objClsEnrollmentDetails.RR = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RR);
                    objClsEnrollmentDetails.RR_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RR));
                    objClsEnrollmentDetails.RR_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RR));
                }
                if (!string.IsNullOrEmpty(clsSaveEmployeeDetailsRequest.RL.Trim()))
                {
                    objClsEnrollmentDetails.RL = Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RL);
                    objClsEnrollmentDetails.RL_ICS = ExtractICS(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RL));
                    objClsEnrollmentDetails.RL_ISO = ExtractISO(Convert.FromBase64String(clsSaveEmployeeDetailsRequest.RL));
                }
            }
            catch (Exception ex)
            {
                //LogManager.WriteErrorLog("GetEmployeeDetailsToSave", ex.ToString());
            }
        }
    }
}