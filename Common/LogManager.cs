using System;
using System.IO;
using System.Linq;
/// <summary>
/// Summary description for LogManager
/// </summary>
namespace Web_API
{
    public static class LogManager
    {
        public static void WriteEventLog(string EventMsg)
        {
            StreamWriter sw = null;
            try
            {
                //Create Directory if Not Exsist
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Event_Logs\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Event_Logs\");
                }

                //Delete Log Files Older than 10 days
                try
                {
                    DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Event_Logs\");
                    FileInfo[] files = info.GetFiles().Where(file =>
                        file.LastWriteTime >= Convert.ToDateTime("01-01-2016") &&
                        file.LastWriteTime <= System.DateTime.Now.AddDays(-10)).ToArray();
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }
                }
                catch (Exception)
                {
                }

                //Write new log
                sw = new StreamWriter(
                    AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Event_Logs\LogFile_" +
                    DateTime.Now.ToString("yyyy-MM-dd HH") + ".Log", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + EventMsg);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(string ErrorMsg)
        {
            StreamWriter sw = null;
            try
            {
                //Create Directory if Not Exsist
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Error_Logs\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Error_Logs\");
                }

                //Delete Log Files Older than 10 days
                try
                {
                    DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Error_Logs\");
                    FileInfo[] files = info.GetFiles().Where(file =>
                        file.LastWriteTime >= Convert.ToDateTime("01-01-2016") &&
                        file.LastWriteTime <= System.DateTime.Now.AddDays(-10)).ToArray();
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }
                }
                catch (Exception)
                {
                }

                //Write new log
                sw = new StreamWriter(
                    AppDomain.CurrentDomain.BaseDirectory + @"\Svc_Error_Logs\LogFile_" +
                    DateTime.Now.ToString("yyyy-MM-dd HH") + ".Log", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ErrorMsg);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}