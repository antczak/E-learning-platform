using E_learning_platform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class CourseController : BaseController
    {
        public ActionResult Add(int? id)
        {
            ViewBag.CatId = id.ToString();
            return View(new CourseAddViewModel() {CourseCategoryList = db.CourseCategories.ToList(), CategoryId = (int)id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Add(CourseAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.Image;
                var course = new Course() {
                    Name = model.Name,
                    Desc = model.Desc,
                    Category = db.CourseCategories.Find(model.CategoryId),
                    Price = model.Price };
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                int id = course.Id;
                var path = AddCourseFile(file, "thumbnail", null, id);
                if (path != null)
                {
                    course.ImagePath = path;
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("View", "Course", new { id = id });
            }
            model.CourseCategoryList = db.CourseCategories.ToList();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            return View(new CourseAddViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Desc = course.Desc,
                Price = course.Price,
                CourseCategoryList = db.CourseCategories.ToList(),
                CategoryId = course.Category.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(CourseAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = model.Image;
                var course = await db.Courses.FindAsync(model.Id);
                course.Category = await db.CourseCategories.FindAsync(model.CategoryId);
                course.Desc = model.Desc;
                course.Name = model.Name;
                course.Price = model.Price;
                int id = course.Id;
                var path = AddCourseFile(file, "thumbnail", null, id);
                if (path != null)
                    course.ImagePath = path;
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = id });
            }
            model.CourseCategoryList = db.CourseCategories.ToList();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EditFullDesc(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            return View(new CourseEditFullDescViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                FullDesc = course.FullDesc
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EditFullDesc(CourseEditFullDescViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = await db.Courses.FindAsync(model.Id);
                if (course == null)
                    return View("Error");
                course.FullDesc = model.FullDesc;
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = course.Id });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            return View(new CourseDeleteViewModel()
            {
                Name = course.Name,
                Desc = course.Desc,
                Id = course.Id,
                Price = course.Price
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(CourseDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = await db.Courses.FindAsync(model.Id);
                if (course == null)
                    return View("Error");
                var catid = course.Category.Id;
                db.Courses.Remove(course);
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Category", new { id = catid });
            }
            return View(model);
        }
        
        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            var count = user.Courses.Where(c => c.Id == course.Id).Count();
            return View(new CourseDetailsViewModel()
            {
                Name = course.Name,
                Id = course.Id,
                hasAccess = count == 1 ? true : false,
                Presentations = course.Presentations.OrderBy(p => p.Order).ThenBy(p => p.Id).ToList(),
                Files = course.Files.OrderBy(f => f.Order).ThenBy(f => f.Id).ToList(),
                Videos = course.Videos.OrderBy(v => v.Order).ThenBy(v => v.Id).ToList(),
                Announcements = course.Announcements.OrderByDescending(a => a.Date).ToList(),
                Tests = course.Tests.Where(t => t.IsVisible == true).OrderBy(t => t.Order).ThenBy(t => t.Id),
                Documents = course.Documents.OrderBy(d => d.Order).ThenBy(d => d.Id).ToList(),
                Moderators = course.Moderators.ToList(),
                FullDesc = course.FullDesc
            });
        }
        public async Task<ActionResult> Buy(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            bool canBuy = true;
            if (user.Balance < course.Price)
                canBuy = false;
            return View(new CourseShortDetailsViewModel()
            {
                Name = course.Name,
                Id = course.Id,
                hasAccess = user.Courses.Contains(course),
                Price = course.Price,
                canBuy = canBuy
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BuyConfirmed(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (user.Balance < course.Price)
                return RedirectToAction("Buy", "Course", new { id = course.Id });
            var u = db.Users.Find(User.Identity.GetUserId());
            u.Balance -= course.Price;
            u.Courses.Add(course);
            await db.SaveChangesAsync();
            return RedirectToAction("View", "Course", new { id = course.Id });
        }
        [Authorize]
        public ActionResult My()
        {
            return View(new CategoryViewModel() {CourseList = user.Courses.ToList()});
        }

    }
}