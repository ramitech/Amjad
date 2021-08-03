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
    public class ClientsController : CMSBaseController
    {

        // GET: CMS/Clients
        public ActionResult Index()
        {
            ViewBag.Title = "العملاء";
            var clients = db.Clients.Where(x => x.IsDeleted == false).OrderByDescending(x => x.ID); 
            return View(clients.ToList());
        }

        // GET: CMS/Clients/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "العملاء";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: CMS/Clients/Create
        public ActionResult Create()
        {
            ViewBag.Title = "العملاء";
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: CMS/Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        client.Image = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + client.Image));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }
                #endregion

                client.UserIdAdd = CurrentAdminId;
                client.Date = DateTime.Now;
                client.IsDeleted = false;

                db.Clients.Add(client);
                db.SaveChanges();
                TempData["msg"] = "s:تمت الإضافة بنجاح";
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", client.LangId);
            return View(client);
        }

        // GET: CMS/Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "العملاء";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", client.LangId);
            ViewBag.UserIdAdd = new SelectList(db.SuperAccounts, "Id", "Name", client.UserIdAdd);
            ViewBag.UserIdDelete = new SelectList(db.SuperAccounts, "Id", "Name", client.UserIdDelete);
            ViewBag.UserIdUpdate = new SelectList(db.SuperAccounts, "Id", "Name", client.UserIdUpdate);
            return View(client);
        }

        // POST: CMS/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (img != null)
                {
                    if (img.ContentType.Contains("image"))
                    {
                        client.Image = Guid.NewGuid() + System.IO.Path.GetExtension(img.FileName);
                        img.SaveAs(Server.MapPath("/Content/Files/" + client.Image));
                    }
                    else
                    {
                        TempData["msg"] = "e:يجب اختيار صورة صحيحة ";
                        return Create();
                    }
                }

                #endregion


                client.UserIdUpdate = CurrentAdminId;
                client.DateUpdate = DateTime.Now;

                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "Name", client.LangId);
            return View(client);
        }

        // GET: CMS/Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "العملاء";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            client.IsDeleted = true;
            client.UserIdDelete = CurrentAdminId;
            client.DateUpdate = DateTime.Now;
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "s:تم الحذف بنجاح";
            return RedirectToAction("Index");
        }

    }
}
