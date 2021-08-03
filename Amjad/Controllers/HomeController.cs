using Amjad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Amjad.Controllers
{
    public class HomeController : Controller
    {
        private AmjadEntities db = new AmjadEntities();
        protected override void Initialize(RequestContext requestContext)
        {
            ViewBag.Sliders = db.Sliders.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
            ViewBag.ContactUsInfo = db.ContactUsInfoes.FirstOrDefault();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-EG");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");

            base.Initialize(requestContext);
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Counters = db.Counters.First();
            ViewBag.Pages = db.Pages.ToList();
            ViewBag.Lawyers = db.Lawyers.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(4).ToList();
            ViewBag.SayAboutUs = db.News.Where(x => x.IsDeleted == false && x.catId == 2).OrderByDescending(x => x.id).Take(3).ToList();
            ViewBag.Clients = db.Clients.Where(x => x.IsDeleted == false).OrderByDescending(x => x.ID).Take(10).ToList();
            ViewBag.News = db.News.Where(x => x.IsDeleted == false && x.catId == 1).OrderByDescending(x => x.id).Take(5).ToList();
            ViewBag.Videos = db.Videos.Where(x => x.IsDeleted == false && x.CatId == 1).OrderByDescending(x => x.ID).Take(5).ToList();
            return View();
        }
        public ActionResult About()
        {
            var item = db.Pages.First(x => x.catId == 2);
            return View(item);
        }
        public ActionResult Page(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Pages.FirstOrDefault(x => x.catId == id);
            if(item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
      
        public ActionResult Specialty()
        {
            var items = db.Pages.Where(x => x.catId >= 7 && x.catId <= 15).ToList();
            return View(items);
        }
        public ActionResult lawyers(int PageId = 1)
        {
            var items = db.Lawyers.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();

            #region Pagination
            int RowsPerPage = 12;
            int TotalCount = items.Count();
            int PageCount = (int)Math.Ceiling((double)TotalCount / RowsPerPage);
            if (PageId < 1)
                PageId = 1;
            if (PageId > PageCount && PageCount > 0)
                PageId = PageCount;
            int StartIndex = (PageId - 1) * RowsPerPage;

            ViewBag.BaseURL = "/Home/lawyers";
            ViewBag.PageCount = PageCount;
            ViewBag.PageId = PageId;
            items = items.Skip(StartIndex).Take(RowsPerPage).ToList();
            #endregion

            return View(items);
        }
        public ActionResult Consult()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactU item)
        {
            if(ModelState.IsValid)
            {
                item.CatId = 1;
                item.Date = DateTime.Now;
                item.IsDeleted = false;
                db.ContactUs.Add(item);
                db.SaveChanges();
                TempData["msg"] = "s: تمت الإضافة بنجاح";
                return RedirectToAction("Contact");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Consult(ContactU item)
        {
            if (ModelState.IsValid)
            {
                item.CatId = 2;
                item.Date = DateTime.Now;
                item.IsDeleted = false;
                db.ContactUs.Add(item);
                db.SaveChanges();
                TempData["msg"] = "s: تمت الإضافة بنجاح";
                return RedirectToAction("Consult");
            }
            return View();
        }


        public ActionResult News(int PageId = 1)
        {            
            var items = db.News.Where(x => x.IsDeleted == false && x.catId == 1).OrderByDescending(x => x.id).ToList();

            #region Pagination
            int RowsPerPage = 10;
            int TotalCount = items.Count();
            int PageCount = (int)Math.Ceiling((double)TotalCount / RowsPerPage);
            if (PageId < 1)
                PageId = 1;
            if (PageId > PageCount && PageCount > 0)
                PageId = PageCount;
            int StartIndex = (PageId - 1) * RowsPerPage;

            ViewBag.BaseURL = "/Home/Articles";
            ViewBag.PageCount = PageCount;
            ViewBag.PageId = PageId;
            items = items.Skip(StartIndex).Take(RowsPerPage).ToList();
            #endregion

            return View(items);
        }
        public ActionResult NewsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.News.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            #region update views count
            item.Views = item.Views + 1;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            #endregion

            return View(item);
        }
     
        public ActionResult Videos(int PageId = 1)
        {
            var items = db.Videos.Where(x => x.IsDeleted == false && x.CatId == 1).OrderByDescending(x => x.ID).ToList();

            #region Pagination
            int RowsPerPage = 10;
            int TotalCount = items.Count();
            int PageCount = (int)Math.Ceiling((double)TotalCount / RowsPerPage);
            if (PageId < 1)
                PageId = 1;
            if (PageId > PageCount && PageCount > 0)
                PageId = PageCount;
            int StartIndex = (PageId - 1) * RowsPerPage;

            ViewBag.BaseURL = "/Home/Videos";
            ViewBag.PageCount = PageCount;
            ViewBag.PageId = PageId;
            items = items.Skip(StartIndex).Take(RowsPerPage).ToList();
            #endregion

            return View(items);
        }
        public ActionResult VideosDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Videos.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            item.VideoName = item.VideoName.Split('/')[item.VideoName.Split('/').Length - 1];

            #region update views count
            item.Views = item.Views + 1;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            #endregion

            return View(item);
        }
      
        public ActionResult Images(int PageId = 1)
        {
            var items = db.Images.Where(x => x.IsDeleted == false && x.CatId == 1 ).OrderByDescending(x => x.ID).ToList();

            #region Pagination
            int RowsPerPage = 12;
            int TotalCount = items.Count();
            int PageCount = (int)Math.Ceiling((double)TotalCount / RowsPerPage);
            if (PageId < 1)
                PageId = 1;
            if (PageId > PageCount && PageCount > 0)
                PageId = PageCount;
            int StartIndex = (PageId - 1) * RowsPerPage;

            ViewBag.BaseURL = "/Home/Images";
            ViewBag.PageCount = PageCount;
            ViewBag.PageId = PageId;
            items = items.Skip(StartIndex).Take(RowsPerPage).ToList();
            #endregion

            return View(items);
        }
     
        public ActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(SuperAccount item, string returnUrl)
        {
            var itemDB = db.SuperAccounts.Where(x => x.Username == item.Username).FirstOrDefault();
            if(itemDB != null)
            {
                if(itemDB.Password.ToLower() == item.Password.Trim().ToLower())
                {
                    Session["AdminId"] = itemDB.Id;
                    if (item.Remember)
                    {
                        Response.Cookies.Add(new HttpCookie("AdminId", "1"));
                        Response.Cookies["AdminId"].Expires = DateTime.Now.AddDays(1);
                    }

                    if (string.IsNullOrEmpty(returnUrl))
                        return Redirect("/CMS/Home");
                    return Redirect(returnUrl);
                }
                else
                {
                    TempData["msg"] = "e: اسم المستخدم أو كلمة المرور غير صحيحة";
                }
            }
            else
            {
                TempData["msg"] = "e: اسم المستخدم أو كلمة المرور غير صحيحة";
            }


            return Login(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}