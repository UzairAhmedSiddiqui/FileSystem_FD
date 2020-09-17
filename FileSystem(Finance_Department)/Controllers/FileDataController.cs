using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileSystem_Finance_Department_.Models;
using System.IO;

using Rotativa;
using FileSystem_Finance_Department_.Context;

namespace FileSystem_Finance_Department_.Controllers
{
    public class FileDataController : Controller
    {
        FileSystemContext db = new FileSystemContext();


        [Authorize(Roles = "Master,Admin,User")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string searchtxt, string fiscalmonth, string fiscalyear, string sectiontxt, string datetxt, int PageNumber = 1)
        {

            var filesview = db.FileDatas.ToList();

            if (User.IsInRole("Master") || User.IsInRole("Admin"))
            {
                filesview = db.FileDatas.ToList();
            }
            else
            {
                filesview = db.FileDatas.Where(x => x.Receivedby.Contains(User.Identity.Name)).ToList();
            }


            if (searchtxt != null && User.IsInRole("User"))
            {
                filesview = db.FileDatas.Where(x => (String.IsNullOrEmpty(searchtxt) || x.Filename.Contains(searchtxt) || x.Addressee.Contains(searchtxt) || x.Date.Contains(searchtxt) ||
                                                        x.Filename.Contains(searchtxt) || x.Filenumber.Contains(searchtxt) ||
                                                        x.Givennumber.Contains(searchtxt) || x.Sectionoforigin.Contains(searchtxt) ||
                                                        x.Subject.Contains(searchtxt) || x.Type.Contains(searchtxt)) && (x.Receivedby.Contains(User.Identity.Name))
                                                        && (String.IsNullOrEmpty(datetxt) || x.Date.Contains(datetxt))).ToList();
            }
            if (searchtxt != null && (User.IsInRole("Master") || User.IsInRole("Admin")))
            {
                filesview = db.FileDatas.Where(x => (String.IsNullOrEmpty(searchtxt) || x.Filename.Contains(searchtxt) || x.Addressee.Contains(searchtxt) || x.Date.Contains(searchtxt) ||
                                                                      x.Filename.Contains(searchtxt) || x.Filenumber.Contains(searchtxt) ||
                                                                      x.Givennumber.Contains(searchtxt) || x.Sectionoforigin.Contains(searchtxt) ||
                                                                      x.Subject.Contains(searchtxt) || x.Type.Contains(searchtxt) || x.Receivedby.Contains(searchtxt))
                                                                      && (String.IsNullOrEmpty(fiscalyear) || x.Date.Contains(fiscalyear))
                                                                      && (String.IsNullOrEmpty(fiscalmonth) || x.Date.Contains(fiscalmonth))).ToList();
            }

            return View(pagination(filesview, PageNumber));
        }

        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }

        [Authorize(Roles = "Master,Admin,User")]
        public ActionResult Details(int id)
        {
            using (FileSystemContext db = new FileSystemContext())
            {
                FileData fd = db.FileDatas.FirstOrDefault(c => c.FileDataId == id);

                var report = new PartialViewAsPdf("~/Views/FileData/Details.cshtml", fd);
                return report;
            }

        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var list = new List<string>() { "Application", "DAKK", "Letter", "Minutes Of Meeting", "Summary", "Notesheet" , "Sanction Order / Release" , "Information" };
            ViewBag.list = list;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Filename,Filenumber,Subject,Type,Givennumber,Pages,Addressee,Sectionoforigin,Receivedby,Status,Pdfdirectory")] FileData fileData,FileData FD, HttpPostedFileBase file,FileDataDetail filedatadetail)
        {
           

            FD.Receivedby = User.Identity.Name.ToString();

            if (file.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(file.FileName);
                _FileName = FD.FileDataId + "-" + FD.Filename + FD.Status + extension;
                string _path = Path.Combine(Server.MapPath("~/App_Data/uploads"), _FileName);
                string uploadedfilename;
                uploadedfilename = _path;
                FD.Pdfdirectory = _FileName;

                // setting values for filedatadetails values
                filedatadetail.FileDataId = FD.FileDataId;
                filedatadetail.FilePath = _path;
                filedatadetail.Status = FD.Status;
                filedatadetail.UploadTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

                file.SaveAs(uploadedfilename);
            }

            try
            {

                if (ModelState.IsValid)
                {
                    db.FileDatas.Add(FD);
                    db.FileDataDetails.Add(filedatadetail);
                    db.SaveChanges();
                    TempData["Msg"] = "Data Successfully Added";
                    return RedirectToAction("Create");
                }
            }
            catch(Exception ex)
            {
                TempData["mess"] = ex.Message;
            }

         

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<FileData> pagination(List<FileData> filesview,int PageNumber)
        {
            ViewBag.TotalPages = Math.Ceiling(filesview.Count() / 4.0);
            ViewBag.PageNumber = PageNumber;

            filesview = filesview.Skip((PageNumber - 1) * 4).Take(4).ToList();

            return filesview;
        }
        public ActionResult Download(string fileName)
        {
            string path = Server.MapPath("~/App_Data/uploads");

            byte[] fileBytes = System.IO.File.ReadAllBytes(path + @"\" + fileName);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    


    }
}
