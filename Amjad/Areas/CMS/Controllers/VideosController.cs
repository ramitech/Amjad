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
    public class VideosController : CMSBaseController
    {

        // GET: CMS/Videos
        public ActionResult Index()
        {
            ViewBag.Title = "مقتطفات قانونية";
            var videos = db.Videos.Where(x => x.IsDeleted == false).OrderByDescending(x => x.ID);
            return View(videos.ToList());
        }

        // GET: CMS/Videos/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "مقتطفات قانونية";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
           
            video.VideoName = video.VideoName.Split('/')[video.VideoName.Split('/').Length - 1];
            return View(video);
        }

        // GET: CMS/Videos/Create
        public ActionResult Create()
        {
            ViewBag.Title = "مقتطفات قانونية";
            return View();
        }

        // POST: CMS/Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Video video)
        {
            if (ModelState.IsValid)
            {
                video.ImageName = "http://img.youtube.com/vi/"+video.VideoName+"/0.jpg";
                video.VideoName = "https://www.youtube.com/embed/" + video.VideoName;
                video.CatId = 1;
                video.UserIdAdd = CurrentAdminId;
                video.Date = DateTime.Now;
                video.IsDeleted = false;
                video.IsBest = false;
                video.Views = 0;
                db.Videos.Add(video);
                db.SaveChanges();
                TempData["msg"] = "s:تمت الإضافة بنجاح";
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: CMS/Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "مقتطفات قانونية";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            video.ImageName = video.ImageName.Split('/')[video.ImageName.Split('/').Length - 2];
            video.VideoName = video.VideoName.Split('/')[video.VideoName.Split('/').Length-1];
            return View(video);
        }

        // POST: CMS/Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Video video)
        {
            if (ModelState.IsValid) 
            {
                video.ImageName = "http://img.youtube.com/vi/" + video.VideoName + "/0.jpg";
                video.VideoName = "https://www.youtube.com/embed/" + video.VideoName;
                video.UserIdUpdate = CurrentAdminId;
                video.DateUpdate = DateTime.Now;                
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: CMS/Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "مقتطفات قانونية";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            video.IsDeleted = true;
            video.UserIdDelete = CurrentAdminId;
            video.DateUpdate = DateTime.Now;
            db.Entry(video).State = EntityState.Modified;      
            db.SaveChanges();
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index");
        }

    }
}
