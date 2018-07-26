using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TigerPaws.Models;


namespace TigerPaws.Controllers
{
    public class CSVController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                //upload csv file
                if (file.ContentLength > 0)
                {   
                    //save uploaded file in the folder
                    string fileName = "UploadedFile";
                    string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    file.SaveAs(path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("Display", "CSV");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        public ActionResult Display()
        {

            string fileName = "UploadedFile";
            string localDestination = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
            //Use CSVReaderHelper to read the file and put it in datatable
            DataTable dt = CSVReaderHelper.GetCSVData(localDestination);

            //Display datatable
            ViewBag.Data = dt;

            return View();
        }

        public ActionResult ExportToXML()
        {          
            string fileName = "UploadedFile";
            string localDestination = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
            //Use CSVReaderHelper to read the file and put it in datatable
            DataTable dt = CSVReaderHelper.GetCSVData(localDestination);

            //Name the table
            dt.TableName = "Product";
            string filePath = Server.MapPath("~/Content/Uploads/writexml.xml");
            //Write xml file and save in the file path
            dt.WriteXml(filePath);
            var saveName = "ProductList" + DateTime.Now.ToString() + ".xml";
            //File download
            return File(Server.MapPath("~/Content/Uploads/writexml.xml"), "application/xml", saveName);


        }
    }    
}
