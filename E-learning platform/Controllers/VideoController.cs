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
    public class VideoController : BaseController
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
            return View(new VideoAddViewModel() { CourseId = course.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(VideoAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.Video;
                var video = new Video() { Name = model.Name, Order = 1, Course = db.Courses.Find(model.CourseId) };
                db.Videos.Add(video);
                await db.SaveChangesAsync();
                int id = video.Course.Id;
                var path = AddCourseFile(file, "video", "Videos", id);
                if (path != null)
                {
                    video.Path = path;
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
            var video = await db.Videos.FindAsync(id);
            if (video == null)
                return View("Error");
            return View(new VideoDetailsViewModel() { Name = video.Name, Path = "/Content/Uploads/" + video.Path, CourseId = video.Course.Id });
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var video = await db.Videos.FindAsync(id);
            if (video == null)
                return View("Error");
            return View(new VideoAddViewModel()
            {
                Id = video.Id,
                Name = video.Name,
                CourseId = video.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(VideoAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var video = await db.Videos.FindAsync(model.Id);
                if (video == null)
                    return View("Error");
                video.Name = model.Name;
                var file = model.Video;
                var path = AddCourseFile(file, "video", "Videos", video.Course.Id);
                if (path != null)
                {
                    video.Path = path;
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
            var video = await db.Videos.FindAsync(id);
            if (video == null)
                return View("Error");
            return View(new VideoDeleteViewModel()
            {
                Name = video.Name,
                Id = video.Id,
                CourseId = video.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(AnnouncementDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var video = await db.Videos.FindAsync(model.Id);
                if (video == null)
                    return View("Error");
                db.Videos.Remove(video);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }
    }
}