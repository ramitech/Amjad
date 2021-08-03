using Amjad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Amjad.Areas.CMS.Controllers
{
  
    public class CMSBaseController : Controller
    {
        protected int CurrentAdminId = 4;
        protected string CurrentAdminName = "admin";
        // GET: CMS/CMSBase
        protected AmjadEntities db = new AmjadEntities();

        protected override void Initialize(RequestContext requestContext)
        {
            if(requestContext.HttpContext.Session["AdminId"] == null || requestContext.HttpContext.Session["AdminId"].ToString() == "")
            {
                if(requestContext.HttpContext.Request.Cookies["adminId"] == null || requestContext.HttpContext.Request.Cookies["adminId"].Value == "")
                {
                    requestContext.HttpContext.Response.Redirect("/Home/Login?returnUrl=" + requestContext.HttpContext.Request.Url.AbsolutePath);
                }
            }
            ViewBag.CurrentAdminId = CurrentAdminId;
            ViewBag.CurrentAdminName = CurrentAdminName;
            var Menu = db.SuperPages.Where(x => x.visible == true && x.IsDeleted == false).OrderBy(x => x.OrderId).ToList();
            ViewBag.Menu = Menu;
            base.Initialize(requestContext);
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