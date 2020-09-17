using FileSystem_Finance_Department_.Context;
using FileSystem_Finance_Department_.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileSystem_Finance_Department_.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        FileSystemContext db = new FileSystemContext();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            var DoesUserExist = db.Logins.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

            if (DoesUserExist != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Index", "FileData");
            }
            else
            {
                TempData["Msg"] = "Username Or Password Is Incorrect";
                return View();
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "Id,Username,Password,UserType")] Login checkuser, string changepassword, string confirmpassword)
        {
            using (FileSystemContext db = new FileSystemContext())
            {

                var DoesUserExist = db.Logins.Where(x => x.Username == checkuser.Username && x.Password == checkuser.Password).FirstOrDefault();

                if (DoesUserExist != null)
                {
                    if (changepassword == confirmpassword)
                    {
                        checkuser.Password = confirmpassword;
                        DoesUserExist.Password = checkuser.Password;
                        db.Entry(DoesUserExist).State = EntityState.Modified;
                        
                        
                        db.SaveChanges();
                        TempData["Msg"] = "Password Change Successfully.";
                        return RedirectToAction("Login", "Account");

                    }
                    else
                    {
                        TempData["Message"] = "Confirm Password Do Not Match.";
                        return View();
                    }

                }
                else
                {
                    TempData["Message"] = "Username or Password Does Not Match or Exist.";
                    return View();
                }
            }
        }
    }
}