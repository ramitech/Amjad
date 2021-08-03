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
    public class ImagesController : CMSBaseController
    {
       // GET: CMS/Images
        public ActionResult Index()
        {
            ViewBag.Title = "معرض الصور";
            var images = db.Images.Where(x=>x.IsDeleted == false).OrderByDescending(x=>x.ID);
            return View(images.ToList());
        }

        // GET: CMS/Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: CMS/Images/Create
        public ActionResult Create()
        {
            ViewBag.Title = "معرض الصور";
            return View();
        }

        // POST: CMS/Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Image image, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        image.ImageName = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + image.ImageName));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }
                else
                {
                    TempData["msg"] = "e:الرجاء اختيار صورة ";
                    return Create();
                }
                #endregion
                image.CatId = 1;
                image.UserIdAdd = CurrentAdminId;
                image.Date = DateTime.Now;
                image.IsDeleted = false;
                db.Images.Add(image);
                db.SaveChanges();
                TempData["msg"] = "s:تمت الإضافة بنجاح";
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: CMS/Images/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "معرض الصور";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: CMS/Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image image, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        image.ImageName = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + image.ImageName));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }
                #endregion

                image.UserIdUpdate = CurrentAdminId;
                image.DateUpdate = DateTime.Now;
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: CMS/Images/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "معرض الصور";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }

            image.IsDeleted = true;
            image.UserIdDelete = CurrentAdminId;
            image.DateUpdate = DateTime.Now;
            db.Entry(image).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index");
        }

    
    }
}
