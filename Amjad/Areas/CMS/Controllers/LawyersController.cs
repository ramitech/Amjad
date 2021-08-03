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
    public class LawyersController : CMSBaseController
    {

        // GET: CMS/Lawyers
        public ActionResult Index()
        {
            ViewBag.Title = "المحامون";
            var lawyers = db.Lawyers.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id);
            return View(lawyers.ToList());
        }

        // GET: CMS/Lawyers/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "المحامون";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyer lawyer = db.Lawyers.Find(id);
            if (lawyer == null)
            {
                return HttpNotFound();
            }
            return View(lawyer);
        }

        // GET: CMS/Lawyers/Create
        public ActionResult Create()
        {
            ViewBag.Title = "المحامون";
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: CMS/Lawyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lawyer lawyer, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        lawyer.Image = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + lawyer.Image));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }

                #endregion

                lawyer.UserIdAdd = CurrentAdminId;
                lawyer.Date = DateTime.Now;
                lawyer.IsDeleted = false;

                db.Lawyers.Add(lawyer);
                db.SaveChanges();
                TempData["msg"] = "s:تمت الإضافة بنجاح";
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", lawyer.LangId);
            return View(lawyer);
        }

        // GET: CMS/Lawyers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "المحامون";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyer lawyer = db.Lawyers.Find(id);
            if (lawyer == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", lawyer.LangId);
            return View(lawyer);
        }

        // POST: CMS/Lawyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lawyer lawyer, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        lawyer.Image = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + lawyer.Image));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }

                #endregion

                lawyer.UserIdUpdate = CurrentAdminId;
                lawyer.DateUpdate = DateTime.Now;

                db.Entry(lawyer).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", lawyer.LangId);
            return View(lawyer);
        }

        // GET: CMS/Lawyers/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "المحامون";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyer lawyer = db.Lawyers.Find(id);
            if (lawyer == null)
            {
                return HttpNotFound();
            }

            lawyer.IsDeleted = true;
            lawyer.UserIdDelete = CurrentAdminId;
            lawyer.DateUpdate = DateTime.Now;
            db.Entry(lawyer).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index");
        }
    }
}
