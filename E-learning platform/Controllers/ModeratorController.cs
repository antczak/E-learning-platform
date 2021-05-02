using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ModeratorController : BaseController
    {
        public ActionResult Index()
        {
            return View("Error");
        }

        public async Task<ActionResult> Add(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            var modRoleId = db.Roles.Where(r => r.Name == "Moderator").FirstOrDefault().Id;
            var moderators = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(modRoleId)).ToList();
            return View(new ModeratorAddViewModel() { ModeratorsList = moderators, CourseId = course.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ModeratorAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modRoleId = db.Roles.Where(r => r.Name == "Moderator").FirstOrDefault().Id;
                var course = db.Courses.Find(model.CourseId);
                var moderator = db.Users.Find(model.ModeratorId);
                if (course == null || moderator == null || !moderator.Roles.Where(r => r.RoleId == modRoleId ).Any())
                    return View("Error");
                course.Moderators.Add(moderator);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = course.Id });
            }
            return View(model);
        }

        public ActionResult Remove(int? id, string ModeratorId)
        {
            if (id == null || String.IsNullOrWhiteSpace(ModeratorId))
                return View("Error");
            var course = db.Courses.Find(id);
            var moderator = db.Users.Find(ModeratorId);
            var modRoleId = db.Roles.Where(r => r.Name == "Moderator").FirstOrDefault().Id;
            if (course == null || moderator == null || !moderator.Roles.Where(r => r.RoleId == modRoleId).Any())
                return View("Error");
            return View(new ModeratorRemoveViewModel()
            {
                ModId = moderator.Id,
                CourseId = course.Id,
                Email = moderator.Email,
                Name = moderator.Name,
                LastName = moderator.LastName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(ModeratorRemoveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modRoleId = db.Roles.Where(r => r.Name == "Moderator").FirstOrDefault().Id;
                var course = db.Courses.Find(model.CourseId);
                var moderator = db.Users.Find(model.ModId);
                if (course == null || moderator == null || !moderator.Roles.Where(r => r.RoleId == modRoleId).Any())
                    return View("Error");
                course.Moderators.Remove(moderator);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = course.Id });
            }
            return View(model);
        }
    }
}