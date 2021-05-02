using E_learning_platform.Filters;
using E_learning_platform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var List = db.CourseCategories.ToList();
            var model = new CourseIndexViewModel() { CategoryList = List };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
                return View("Error");
            var category = await db.CourseCategories.FindAsync(id);
            var cat = db.CourseCategories.Find(id);
            return View(new CategoryViewModel() { CourseList = category.Courses.ToList(), Id = (int)id, Name = cat.Name });
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.CourseCategories.Add(new CourseCategory() { Name = model.Name, Desc = model.Desc });
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Category");
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var category = await db.CourseCategories.FindAsync(id);
            return View(new CategoryAddViewModel() { Id = (int)id, Name = category.Name, Desc = category.Desc });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await db.CourseCategories.FindAsync(model.Id);
                category.Name = model.Name;
                category.Desc = model.Desc;
                await db.SaveChangesAsync();
                int id = category.Id;
                return RedirectToAction("View", "Category", new { id = id });
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return View("Error");
            var category = await db.CourseCategories.FindAsync(id);
            return View(new CategoryDeleteViewModel() { Name = category.Name, Desc = category.Desc, Id = category.Id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CategoryDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await db.CourseCategories.FindAsync(model.Id);
                if (category == null)
                    return View("Error");
                db.CourseCategories.Remove(category);
                var courses = await db.Courses.Where(c => c.Category.Id == category.Id).ToListAsync();
                db.Courses.RemoveRange(courses);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Category");
            }
            return View(model);
        }
    }
}