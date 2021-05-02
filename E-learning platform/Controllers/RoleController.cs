using E_learning_platform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : BaseController
    {
        public ActionResult Index()
        {
            return View(new RoleIndexViewModel() {RoleList = db.Roles.ToList()});
        }

        public ActionResult Details(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return View("Error");
            var role = db.Roles.Find(id);
            if (role == null)
                return View("Error");
            var users = db.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(id))
                .ToList();
            return View(new RoleDetailsViewModel() { UserList = users, Name = role.Name, Id = role.Id });
        }
        public ActionResult Assign(string id, string Role, string Email)
        {
            var roleName = Role;
            var email = Email;
            if (!String.IsNullOrWhiteSpace(roleName) && !String.IsNullOrWhiteSpace(email))
            {
                var role = db.Roles.Where(r => r.Name == roleName).FirstOrDefault();
                if (role == null)
                    return View("Error");
                return View(new RoleAssignViewModel() { Name = role.Name, RoleId = role.Id, Email = email });
            }
            else
            {
                if (String.IsNullOrWhiteSpace(id))
                    return View("Error");
                var role = db.Roles.Find(id);
                if (role == null)
                    return View("Error");
                return View(new RoleAssignViewModel() { Name = role.Name, RoleId = role.Id });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(RoleAssignViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var role = db.Roles.Find(model.RoleId);
                var user = userManager.FindByEmail(model.Email);
                if (role == null)
                    return View("Error");
                if (user == null)
                {
                    model.Name = role.Name;
                    ModelState.AddModelError("", "Taki użytkownik nie istnieje");
                    return View(model);
                }
                userManager.AddToRole(user.Id, role.Name);
                return RedirectToAction("Details", new { id = model.RoleId });
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (String.IsNullOrWhiteSpace(id))
                return View("Error");
            var role = db.Roles.Find(id);
            if (role == null)
                return View("Error");
            var userId = Request.QueryString["uid"];
            if (String.IsNullOrWhiteSpace(userId))
                return View("Error");
            var user = userManager.FindById(userId);
            if (user == null)
                return View("Error");
            userManager.RemoveFromRole(user.Id, role.Name);
            return RedirectToAction("Details", new { id = id });
        }
    }
}