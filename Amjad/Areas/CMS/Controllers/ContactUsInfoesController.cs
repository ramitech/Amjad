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
    public class ContactUsInfoesController : CMSBaseController
    {

      
        // GET: CMS/ContactUsInfoes/Edit/5
        public ActionResult Index()
        {
            ViewBag.Title = "قنوات التواصل";
            
            ContactUsInfo contactUsInfo = db.ContactUsInfoes.FirstOrDefault();
            if (contactUsInfo == null)
            {
                return HttpNotFound();
            }
            return View(contactUsInfo);
        }

        // POST: CMS/ContactUsInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(ContactUsInfo contactUsInfo)
        {
            if (ModelState.IsValid)
            {
                contactUsInfo.UserIdUpdate = CurrentAdminId;
                contactUsInfo.Date = DateTime.Now;

                db.Entry(contactUsInfo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            return View(contactUsInfo);
        }


    }
}
