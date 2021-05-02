using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize]
    public class FileController : BaseController
    {
        public ActionResult Index()
        {
            return View("Error");
        }
        public async Task<ActionResult> Download(int? id)
        {
            if (id == null)
                return View("Error");
            var file = await db.Files.FindAsync(id);
            if (file == null)
                return View("Error");
            var fullPath = Path.Combine(Server.MapPath("~/Content/Uploads"), file.Path);
            return File(fullPath, MediaTypeNames.Application.Octet, Path.GetFileName(fullPath));
        }
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            return View(new FileAddViewModel() { CourseId = course.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(FileAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.File;
                var cFile = new CFile() { Name = model.Name, Order = 1, Course = db.Courses.Find(model.CourseId) };
                db.Files.Add(cFile);
                await db.SaveChangesAsync();
                int id = cFile.Course.Id;
                var path = AddCourseFile(file, "file", "Files", id);
                if (path != null)
                {
                    cFile.Path = path;
                    await db.SaveChangesAsync();
                }             
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var file = await db.Files.FindAsync(id);
            if (file == null)
                return View("Error");
            return View(new FileAddViewModel()
            {
                Id = file.Id,
                Name = file.Name,
                CourseId = file.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(FileAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = await db.Files.FindAsync(model.Id);
                if (file == null)
                    return View("Error");
                file.Name = model.Name;
                var postedFile = model.File;
                var path = AddCourseFile(postedFile, "file", "Files", file.Course.Id);
                if (path != null)
                {
                    file.Path = path;
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
            var file = await db.Files.FindAsync(id);
            if (file == null)
                return View("Error");
            return View(new FileDeleteViewModel()
            {
                Name = file.Name,
                Id = file.Id,
                CourseId = file.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(FileDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = await db.Files.FindAsync(model.Id);
                if (file == null)
                    return View("Error");
                db.Files.Remove(file);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }
    }
}