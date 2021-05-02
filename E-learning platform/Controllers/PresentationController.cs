using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize]
    public class PresentationController : BaseController
    {
        public ActionResult Index()
        {
            return View("Error");
        }
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            return View(new PresentationAddViewModel() { CourseId = course.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(PresentationAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.Presentation;
                var presentation = new Presentation() { Name = model.Name, Order = 1, Course = db.Courses.Find(model.CourseId) };
                db.Presentations.Add(presentation);
                await db.SaveChangesAsync();
                int id = presentation.Course.Id;
                var path = AddCourseFile(file, "presentation", "Presentations", id);
                if (path != null)
                {
                    presentation.Path = path;
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }
        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
                return View("Error");
            var presentation = await db.Presentations.FindAsync(id);
            if (presentation == null)
                return View("Error");
            return View(new PresentationDetailsViewModel() { Name = presentation.Name, Path = "Uploads/" + presentation.Path, CourseId = presentation.Course.Id });
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var presentation = await db.Presentations.FindAsync(id);
            if (presentation == null)
                return View("Error");
            return View(new PresentationAddViewModel()
            {
                Id = presentation.Id,
                Name = presentation.Name,
                CourseId = presentation.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(PresentationAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var presentation = await db.Presentations.FindAsync(model.Id);
                if (presentation == null)
                    return View("Error");
                presentation.Name = model.Name;
                var file = model.Presentation;
                var path = AddCourseFile(file, "presentation", "Presentations", presentation.Course.Id);
                if (path != null)
                {
                    presentation.Path = path;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return View("Error");
            var presentation= await db.Presentations.FindAsync(id);
            if (presentation == null)
                return View("Error");
            return View(new PresentationDeleteViewModel()
            {
                Name = presentation.Name,
                Id = presentation.Id,
                CourseId = presentation.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(PresentationDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var presentation = await db.Presentations.FindAsync(model.Id);
                if (presentation == null)
                    return View("Error");
                db.Presentations.Remove(presentation);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }
    }
}