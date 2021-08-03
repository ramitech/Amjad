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
    public class CountersController : CMSBaseController
    {


        public ActionResult Index()
        {
            ViewBag.Title = "احصائيات";
          
            Counter counter = db.Counters.FirstOrDefault();
            if (counter == null)
            {
                return HttpNotFound();
            }
            return View(counter);
        }

        // POST: CMS/Counters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Counter counter)
        {
            ViewBag.Title = "احصائيات";
            if (ModelState.IsValid)
            {
                counter.UserIdUpdate = CurrentAdminId;
                counter.Date = DateTime.Now;
                db.Entry(counter).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            return View(counter);
        }

    }
}
