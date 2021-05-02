using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize(Roles = "Administrator,Moderator")]
    public class AnnouncementController : BaseController
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
            return View(new AnnouncementAddViewModel() { CourseId = course.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AnnouncementAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = await db.Courses.FindAsync(model.CourseId);
                if (course == null)
                    return View("Error");
                var announcement = new Announcement() { Title = model.Title, Text = model.Text, Course = course, Date = DateTime.Now };
                db.Announcements.Add(announcement);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var announcement = await db.Announcements.FindAsync(id);
            if (announcement == null)
                return View("Error");
            return View(new AnnouncementAddViewModel()
            {
                Title = announcement.Title,
                Text = announcement.Text,
                Id = announcement.Id,
                CourseId = announcement.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AnnouncementAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var announcement = await db.Announcements.FindAsync(model.Id);
                if (announcement == null)
                    return View("Error");
                announcement.Text = model.Text;
                announcement.Title = model.Title;
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return View("Error");
            var announcement = await db.Announcements.FindAsync(id);
            if (announcement == null)
                return View("Error");
            return View(new AnnouncementDeleteViewModel()
            {
                Title = announcement.Title,
                Id = announcement.Id,
                CourseId = announcement.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(AnnouncementDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var announcement = await db.Announcements.FindAsync(model.Id);
                if (announcement == null)
                    return View("Error");
                db.Announcements.Remove(announcement);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

    }
}