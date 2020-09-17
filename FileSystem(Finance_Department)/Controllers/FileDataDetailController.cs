using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileSystem_Finance_Department_.Context;
using FileSystem_Finance_Department_.Models;
using System.IO;

namespace FileSystem_Finance_Department_.Controllers
{
    public class FileDataDetailController : Controller
    {
        private FileSystemContext db = new FileSystemContext();
       

        // GET: FileDataDetail
        public ActionResult Index(int? filedataid)
        {
            var fileid = filedataid;

                TempData["FileID"] = fileid;

                var fileDataDetails = db.FileDataDetails.Where(x => x.FileDataId == fileid).ToList();
                return View(fileDataDetails);
           
          
        }

        // GET: FileDataDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileDataDetail fileDataDetail = db.FileDataDetails.Find(id);
            if (fileDataDetail == null)
            {
                return HttpNotFound();
            }
            return View(fileDataDetail);
        }

        // GET: FileDataDetail/Create
        public ActionResult Create(int id)
        {
            return View(new FileDataDetail() {FileDataId=id });
        }

        // POST: FileDataDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileDataDetailId,FileDataId,FilePath,Status,UploadTime")] FileDataDetail fileDataDetail, FileDataDetail fDD, HttpPostedFileBase fileupload)
        {
            if (fileupload.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(fileupload.FileName);

                string extension = Path.GetExtension(fileupload.FileName);
                _FileName = fDD.FileDataId + "-" + fDD.Status + extension;
                string _path = Path.Combine(Server.MapPath("~/App_Data/uploads"), _FileName);

                fDD.FilePath = _path;

                fDD.UploadTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

                fileupload.SaveAs(fDD.FilePath);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    db.FileDataDetails.Add(fDD);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { filedataid = fDD.FileDataId });
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
           
           

            return View();
        }

        // GET: FileDataDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileDataDetail fileDataDetail = db.FileDataDetails.Find(id);
            if (fileDataDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileDataId = new SelectList(db.FileDatas, "FileDataId", "Date", fileDataDetail.FileDataId);
            return View(fileDataDetail);
        }

        // POST: FileDataDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileDataDetailId,FileDataId,FilePath,Status,UploadTime")] FileDataDetail fileDataDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileDataDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FileDataId = new SelectList(db.FileDatas, "FileDataId", "Date", fileDataDetail.FileDataId);
            return View(fileDataDetail);
        }

        // GET: FileDataDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileDataDetail fileDataDetail = db.FileDataDetails.Find(id);
            if (fileDataDetail == null)
            {
                return HttpNotFound();
            }
            return View(fileDataDetail);
        }

        // POST: FileDataDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileDataDetail fileDataDetail = db.FileDataDetails.Find(id);
            db.FileDataDetails.Remove(fileDataDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
