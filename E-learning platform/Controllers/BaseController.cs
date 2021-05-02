using E_learning_platform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db;
        protected ApplicationUser user;
        public BaseController()
        {
            db = ApplicationDbContext.Create();
            user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        protected string AddCourseFile(HttpPostedFileBase file, string prefix, string folder, int courseId)
        {
            if (file != null)
            {
                var filename = prefix+ "_" + GetTimestamp(DateTime.Now) + "_" + Path.GetFileName(file.FileName);
                var path = "Courses/" + courseId + "/";
                if (folder != null)
                    path += folder;
                var filepath = Path.Combine(path, filename);
                var serverPath = Server.MapPath("~/Content/Uploads");
                Directory.CreateDirectory(Path.Combine(serverPath, path));
                file.SaveAs(Path.Combine(serverPath, path, filename));
                return filepath;
            }
            return null;
        }

       protected String GetTimestamp(DateTime value)
       {
           return value.ToString("yyyyMMddHHmmssffff");
       }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}