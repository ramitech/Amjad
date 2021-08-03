using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Amjad.Models;

namespace Amjad.Areas.CMS.Controllers
{
    public class ContactUsController : CMSBaseController
    {

        // GET: CMS/ContactUs
        public ActionResult Index(int id)
        {
            var contactUs = db.ContactUs.Where(x => x.CatId == id && x.IsDeleted == false).OrderByDescending(x => x.ID).ToList();
            ViewBag.Title = db.ContactUsCategories.Find(id).Name;

            return View(contactUs.ToList());
        }

        // GET: CMS/ContactUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactU contactU = db.ContactUs.Find(id);
            if (contactU == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = contactU.ContactUsCategory.Name;
            return View(contactU);
        }

        // GET: CMS/ContactUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactU contactU = db.ContactUs.Find(id);
            if (contactU == null)
            {
                return HttpNotFound();
            }
            contactU.IsDeleted = true;
            contactU.UserIdDelete = CurrentAdminId;
            db.Entry(contactU).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Title = contactU.ContactUsCategory.Name;
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index",new {@id = contactU.CatId });
        }

    }
}
