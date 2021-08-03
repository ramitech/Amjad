using Amjad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amjad.Areas.CMS.Controllers
{
    public class HomeController : CMSBaseController
    {
        // GET: CMS/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cookies["AdminId"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("/Home/Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword item)
        {
            var itemDB = db.SuperAccounts.Find(CurrentAdminId);
            if(itemDB != null)
            {
                if(itemDB.Password.ToLower() == item.Password.ToLower())
                {
                    itemDB.Password = item.PasswordNew;
                    db.Entry(itemDB).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "s: تم تعيير كلمة المرور بنجاح";
                    //return Logout();
                }
                else
                {
                    TempData["msg"] = "e: كلمة المرور غير صحيحة";
                }

                return RedirectToAction("ChangePassword");
            }
            else
            {
                return Logout();
            }
        }
    }
}