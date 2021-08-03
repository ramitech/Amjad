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
    public class PagesController : CMSBaseController
    {

        // GET: CMS/Pages/Edit/5
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Where(x=>x.catId == id).FirstOrDefault();
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = page.PagesCategory.Name;
            ViewBag.CatId = new SelectList(db.PagesCategories, "Id", "Name", page.catId);
            return View(page);
        }

        // POST: CMS/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(Page page, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var pageDB = db.Pages.Find(page.id);

                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        pageDB.ImageName = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + pageDB.ImageName));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return RedirectToAction("Index", new { @id = pageDB.catId });
                    }
                }
                #endregion
                pageDB.Title = page.Title;
                pageDB.Summary = page.Summary;
                pageDB.Details = page.Details;
                pageDB.UserIdUpdate = CurrentAdminId;
                pageDB.Date = DateTime.Now;
                db.Entry(pageDB).State = EntityState.Modified;

                db.SaveChanges();

                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index", new { @id = pageDB.catId });
            }
            ViewBag.CatId = new SelectList(db.PagesCategories, "Id", "Name", page.catId);
            return View(page);
        }

        // GET: CMS/Pages/Delete/5
   
    }
}
