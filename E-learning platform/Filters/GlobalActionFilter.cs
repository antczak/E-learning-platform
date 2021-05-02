using E_learning_platform.Controllers;
using E_learning_platform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace E_learning_platform.Filters
{
    public class GlobalActionFilter: ActionFilterAttribute, IActionFilter, IResultFilter
    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var db = ApplicationDbContext.Create();
            var userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            bool isAdmin = false;
            bool isModerator = false;
            if (user != null)
            {
                filterContext.Controller.ViewBag.Balance = user.Balance;
                if (userManager.IsInRole(user.Id, "Administrator"))
                    isAdmin = true;
            }
            string[] controllers = {"Document", "File", "Presentation", "Test", "Video", "Announcement" };
            string[] actions = {"View", "Download", "Solve"};
            string[] specialactions = { "Add", "EditFullDesc" };
            string[] Ignoreactions = {"Check", "Result"};
            if (user != null && filterContext.RouteData.Values["id"]!= null && !isAdmin && System.Web.HttpContext.Current.User.Identity.IsAuthenticated && controllers.Contains(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName) && !Ignoreactions.Contains(filterContext.ActionDescriptor.ActionName))
            {
                IEnumerable<ICourseItem> workplace = null;
                switch (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName)
                {
                    case "Presentation": workplace = db.Presentations; break;
                    case "Video": workplace = db.Videos; break;
                    case "Document": workplace = db.Documents; break;
                    case "File": workplace = db.Files; break;
                    case "Test": workplace = db.Tests; break;
                }
                int id;
                bool parse = Int32.TryParse(filterContext.RouteData.Values["id"].ToString(), out id);
                if (!parse)
                    id = 0;
                ICourseItem courseItem;
                int courseId;
                if (specialactions.Contains(filterContext.ActionDescriptor.ActionName))
                {
                    courseId = id;
                }
                else
                {
                    courseItem = workplace.Where(i => i.Id == id).FirstOrDefault();
                    courseId = courseItem.Course.Id;
                }
                if (actions.Contains(filterContext.ActionDescriptor.ActionName))
                {
                    if (!user.Courses.Where(c => c.Id == courseId).Any() && !user.ModeratedCourses.Where(c => c.Id == courseId).Any())
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                            {
                                {"controller", "Course"},
                                {"action", "Buy"},
                                {"id", courseId}
                            });
                        return;
                    }
                }
                else
                {
                    if (!user.ModeratedCourses.Where(c => c.Id == courseId).Any())
                    {
                        filterContext.Result = new ViewResult() { ViewName = "Error" };
                        return;
                    }
                    else
                    {
                        isModerator = true;
                    }
                }
            }
            else if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Course") && (filterContext.ActionDescriptor.ActionName.Equals("View")))
            {
                int id;
                try
                {
                    id = Int32.Parse(filterContext.RouteData.Values["id"].ToString());
                }
                catch (Exception e)
                {
                    id = 1;
                }
                int courseId = db.Courses.Find(id).Id;
                if (user.ModeratedCourses.Where(c => c.Id == courseId).Any())
                {
                    isModerator = true;
                }

            }
            filterContext.Controller.ViewBag.isAdmin = isAdmin;
            filterContext.Controller.ViewBag.isModerator = isModerator;
            this.OnActionExecuting(filterContext);
        }

    }
}