using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PDFConvert
{
    public class Helper
    {
        public static string GetContentType(string fileExtension)
        {
            string contentType = "";
            switch (fileExtension.ToLower())
            {
                case "doc":
                    contentType = "application/msword";
                    break;
                case "docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "xls":
                    contentType = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "pdf":
                    contentType = "application/pdf";
                    break;
                case "ppt":
                    contentType = "application/vnd.ms-powerpoint";
                    break;
                case "pptx":
                    contentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case "jpg":
                    contentType = "image/jpeg";
                    break;
                case "jpeg":
                    contentType = "image/jpeg";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                case "svg":
                    contentType = "image/svg+xml";
                    break;
                case "bmp":
                    contentType = "image/bmp";
                    break;
                case "gif":
                    contentType = "image/gif";
                    break;
                case "ico":
                    contentType = "image/vnd.microsoft.icon";
                    break;
                case "tif":
                    contentType = "image/tiff";
                    break;
                case "tiff":
                    contentType = "image/tiff";
                    break;
                case "webp":
                    contentType = "image/webp";
                    break;
                case "txt":
                    contentType = "text/plain";
                    break;
                default:
                    contentType = "text/plain";
                    break;
            }
            return contentType;
        }

        public static void ErrorLog(string errMsg)
        {

            string LogPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            if (!Directory.Exists(LogPath + @"\Log"))
                Directory.CreateDirectory(LogPath + @"\Log");

            string repeatChar = new string('*', 50);

            File.AppendAllText(LogPath + @"\Log\Log_" + DateTime.Now.ToString("dd_MMM_yyyy") + ".txt", repeatChar + Environment.NewLine + "Error Date and time: " + DateTime.Now.ToString() + Environment.NewLine + errMsg + Environment.NewLine + repeatChar + Environment.NewLine);

        }

        public static void DeleteFile(string _path)
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }

        public static void DisplaySweetAlertPopup(Page pg, string title, string message, MessageType messagetype)
        {
            try
            {
                string tScript = "showPopup('" + title + "', '" + message + "', '" + messagetype + "')";
                ScriptManager.RegisterStartupScript(pg, pg.GetType(), "msg", tScript, true);
            }
            catch (Exception) { }
        }

        public static void DisplayPopWithoutConfirmation(Page pg, string title, string message, MessageType messagetype)
        {
            try
            {
                string tScript = "DisplayPopWithoutConfirmation('" + title + "', '" + message + "', '" + messagetype + "')";
                ScriptManager.RegisterStartupScript(pg, pg.GetType(), "msg", tScript, true);
            }
            catch (Exception) { }
        }
    }
    public enum MessageType
    {
        success = 1,
        error = 2,
        info = 3,
    }
}