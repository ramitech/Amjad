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
    public class NewsController : CMSBaseController
    {
        // GET: CMS/News
        public ActionResult Index(int id)
        {
            var news = db.News.Where(x => x.IsDeleted == false && x.catId == id).OrderBy(x => x.catId).OrderByDescending(x => x.id).ToList();
            ViewBag.Title = db.NewsCategories.Find(id).Name;
            return View(news.ToList());
        }

        // GET: CMS/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = news.NewsCategory.Name;
            return View(news);
        }

        // GET: CMS/News/Create
        public ActionResult Create(int id)
        {
            ViewBag.CatId = new SelectList(db.NewsCategories, "Id", "Name", id);
            ViewBag.Title = db.NewsCategories.Find(id).Name;
            return View();
        }

        // POST: CMS/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News news, HttpPostedFileBase img, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                #region Upload File
                if (file != null)
                {
                    news.FileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("/Content/Files/" + news.FileName));
                }

                #endregion

                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        news.ImageName = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + news.ImageName));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create(news.catId.Value);
                    }
                }
                #endregion

                news.UserIdAdd = CurrentAdminId;
                news.Date = DateTime.Now;
                news.IsDeleted = false;
                news.IsBest = false;
                news.Views = 0;

                db.News.Add(news);
                db.SaveChanges();
                TempData["msg"] = "s:تمت الإضافة بنجاح";
                return RedirectToAction("Index",new {@id = news.catId });
            }
            ViewBag.CatId = new SelectList(db.NewsCategories, "Id", "Name", news.catId);
            return View(news);
        }

        // GET: CMS/News/Edit/5
        public ActionResult Edit(int? id)
        {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            ViewBag.CatId = new SelectList(db.NewsCategories, "Id", "Name", news.catId);
            ViewBag.Title = news.NewsCategory.Name;
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: CMS/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News news, HttpPostedFileBase img, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                #region Upload File
                if (file != null)
                {
                    news.FileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("/Content/Files/" + news.FileName));
                }

                #endregion

                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        news.ImageName = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + news.ImageName));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create(news.catId.Value);
                    }
                }
                #endregion

                news.UserIdUpdate = CurrentAdminId;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @id = news.catId });
            }
            ViewBag.CatId = new SelectList(db.NewsCategories, "Id", "Name", news.catId);
            return View(news);
        }

        // GET: CMS/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            news.IsDeleted = true;
            news.UserIdDelete = CurrentAdminId;
            news.DateUpdate = DateTime.Now;
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Title = news.NewsCategory.Name;
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index", new { @id = news.catId });
        }

    }
}
