using System;
using System.IO;
using System.Net;

namespace ShopBridge.Repository.Entities
{
    /// <summary>
    /// This class will be used for logging exception
    /// </summary>
    public static class LogException
    {
        private static string errorlineNo, errormsg, exceptionType, hostIP, errorLocation, hostName, stackTrackInformation;
        private static string fileExtension = ".txt";
        private static string exceptionFolderName = "Error";
        private static string dateFormatyyMMyyyy = "ddMMyyyy";
        private static string binDebugFolder = "bin/debug";        
        private static string file = DateTime.Today.ToString(dateFormatyyMMyyyy);

        public static string FolderPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(binDebugFolder, string.Empty), exceptionFolderName);
            }
        }

        public static void Log(Exception ex, string additionalInformation = "please try later!")
        {
            var line = Environment.NewLine + Environment.NewLine;

            errorlineNo = ex.StackTrace?.Substring(ex.StackTrace.Length - 7, 7);
            errormsg = ex.GetType()?.Name?.ToString();
            stackTrackInformation = ex.StackTrace?.ToString();
            exceptionType = ex.GetType()?.ToString();
            errorLocation = ex.Message?.ToString();
            hostName = Dns.GetHostName();
            hostIP = Dns.GetHostByName(hostName)?.AddressList[0]?.ToString();

            try
            {
                if (!Directory.Exists(FolderPath))                
                    Directory.CreateDirectory(FolderPath);

                var filepath = Path.Combine(FolderPath, file) + fileExtension;

                if (!File.Exists(filepath))                
                    File.Create(filepath).Dispose();                

                using (StreamWriter sw = File.AppendText(filepath))
                { 
                    string errorMsg = @"Log Written Date :" + " " + DateTime.Now.ToString() + line +
                                       "Error occoured at Line No :" + " " + errorlineNo + line +
                                       "Stacktrace Information :" + " " + stackTrackInformation + line +
                                       "Error Message :" + " " + errormsg + line +
                                       "Exception Type :" + " " + exceptionType + line +
                                       "Error Location :" + " " + errorLocation + line +                                    
                                       "User Host Name :" + " " + hostName + line +
                                       "User Host IP :" + " " + hostIP + line +
                                       "Additional Information :" + " " + additionalInformation;

                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine("-----------Exception Details on : " + " " + DateTime.UtcNow.ToString() + "-----------------");
                    sw.WriteLine();
                    sw.WriteLine(errorMsg);
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(Environment.NewLine);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
