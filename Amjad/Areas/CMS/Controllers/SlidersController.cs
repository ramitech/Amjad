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
    public class SlidersController : CMSBaseController
    {

        // GET: CMS/Sliders
        public ActionResult Index()
        {
            ViewBag.Title = "السليدر";
            var sliders = db.Sliders.Where(x => x.IsDeleted == false).OrderByDescending(x=>x.Id);
            return View(sliders.ToList());
        }

        // GET: CMS/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "السليدر";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: CMS/Sliders/Create
        public ActionResult Create()
        {
            ViewBag.Title = "السليدر";
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: CMS/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        slider.Image = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + slider.Image));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }
                #endregion

                slider.UserIdAdd = CurrentAdminId;
                slider.Date = DateTime.Now;
                slider.IsDeleted = false;

                db.Sliders.Add(slider);
                db.SaveChanges();
                TempData["msg"] = "s:تمت الإضافة بنجاح";
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", slider.LangId);
            return View(slider);
        }

        // GET: CMS/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "السليدر";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: CMS/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        slider.Image = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + slider.Image));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }
                #endregion

                slider.UserIdUpdate = CurrentAdminId;
                slider.DateUpdate = DateTime.Now;

                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", slider.LangId);
            return View(slider);
        }

        // GET: CMS/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "السليدر";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }

            slider.IsDeleted = true;
            slider.UserIdDelete = CurrentAdminId;
            slider.DateUpdate = DateTime.Now;
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index");

        }

       
    }
}
