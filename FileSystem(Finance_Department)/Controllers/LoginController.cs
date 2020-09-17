using FileSystem_Finance_Department_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileSystem_Finance_Department_.Controllers
{
    public class LoginController : Controller
    {
        private FileSystemFDEntities db = new FileSystemFDEntities();
        // GET: Login
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
                FormsAuthentication.SetAuthCookie(user.UserType, false);
                return RedirectToAction("Index", "FileData");
            }
            else
            {
                TempData["Msg"] = "Username Or Password Is Incorrect";
                return View();
            }

        }
    }
}