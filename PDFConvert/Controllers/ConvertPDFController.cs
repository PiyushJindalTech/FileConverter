using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Imagine_Converter;
using System.IO;


namespace PDFConvert.Controllers
{
    public class ConvertPDFController : Controller
    {
        beFileConverter fileConverter = new beFileConverter();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            TempData["id"] = id;
            TempData.Keep();
            return View();

        }

        [HttpPost]
        public void ConvertFile(HttpPostedFileBase flpExcel)
        {
            try
            {
                string id = "";
                if (TempData["id"] != null)
                    id = Convert.ToString(TempData["id"]);

                string filePath = Server.MapPath("~/ExportFiles/");
                flpExcel.SaveAs(filePath + flpExcel.FileName);
                string outputFileName = "";
                string fileExtension = Path.GetExtension(flpExcel.FileName).ToLower().Replace(".", "");
                string tStatus = "";

                if (fileExtension == "doc" || fileExtension == "docx")
                    tStatus = fileConverter.ExportWordToPdf(filePath, flpExcel.FileName, out outputFileName);

                else if (fileExtension == "xls" || fileExtension == "xlsx" || fileExtension == "ods" || fileExtension == "csv")
                    tStatus = fileConverter.ExportExcelToPdf(filePath, flpExcel.FileName, out outputFileName);

                else if (fileExtension == "ppt" || fileExtension == "pptx")
                    tStatus = fileConverter.ExportPowerPointToPdf(filePath, flpExcel.FileName, out outputFileName);

                else if (fileExtension == "jpg" || fileExtension == "jpeg" || fileExtension == "png" || fileExtension == "svg" || fileExtension == "gif")
                    tStatus = fileConverter.ExportToImageToPdf(filePath, flpExcel.FileName, out outputFileName);

                else if (fileExtension == "pdf")
                {
                    if (id == "PDFtoWord")
                        tStatus = fileConverter.ExportPdfToWord(filePath, flpExcel.FileName, out outputFileName);

                    else if (id == "PDFtoExcel")
                        tStatus = fileConverter.ExportPdfToExcel(filePath, flpExcel.FileName, out outputFileName);

                    //else if (id == "PDFtoPPT")
                    //    tStatus = fileConverter.ExportPdfToPowerPoint(filePath, flpExcel.FileName, out outputFileName);

                    else
                        tStatus = "";
                }

                else
                    tStatus = "";

                if (tStatus != "1")
                    Helper.ErrorLog("ConvertFile(): " + tStatus);
                else
                    DownloadFile(filePath, outputFileName, Helper.GetContentType(outputFileName.Split('.').Last()));
            }
            catch (Exception ex)
            {
                Helper.ErrorLog(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public void DownloadFile(string sourcePath, string fileName, string contentType)
        {
            try
            {
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(sourcePath + fileName);
                Response.Flush();
                Response.SuppressContent = true;
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                Helper.DeleteFile(sourcePath + fileName);
            }
            catch (Exception ex)
            {
                Helper.ErrorLog(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }


    }
}
