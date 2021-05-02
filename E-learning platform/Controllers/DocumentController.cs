using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize]
    public class DocumentController : BaseController
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
            return View(new DocumentAddViewModel() { CourseId = course.Id });
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(DocumentAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = await db.Courses.FindAsync(model.CourseId);
                if (course == null)
                    return View("Error");
                var document = new Document() { Name = model.Name, Text = model.Text, Course = course, Date = DateTime.Now };
                db.Documents.Add(document);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
                return View("Error");
            var document = await db.Documents.FindAsync(id);
            if (document == null)
                return View("Error");
            return View(new DocumentDetailsViewModel() { Name = document.Name, Text = document.Text, Date = document.Date, CourseId = document.Course.Id});
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var document = await db.Documents.FindAsync(id);
            if (document == null)
                return View("Error");
            return View(new DocumentAddViewModel()
            {
                Name = document.Name,
                Text = document.Text,
                Id = document.Id,
                CourseId = document.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(DocumentAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var document = await db.Documents.FindAsync(model.Id);
                if (document == null)
                    return View("Error");
                document.Text = model.Text;
                document.Name = model.Name;
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
            var document = await db.Documents.FindAsync(id);
            if (document == null)
                return View("Error");
            return View(new DocumentDeleteViewModel()
            {
                Name = document.Name,
                Id = document.Id,
                CourseId = document.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(DocumentDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var document = await db.Documents.FindAsync(model.Id);
                if (document == null)
                    return View("Error");
                db.Documents.Remove(document);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }
    }
}