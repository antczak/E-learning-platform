using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        // GET: Profile
        public ActionResult Index()
        {
            return RedirectToAction("My");
        }

        public ActionResult Details(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return RedirectToAction("My");
            var user = db.Users.Find(id);
            if (user == null)
                return RedirectToAction("My");
            return View(new ProfileDetailsViewModel()
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CourseList = user.Courses.ToList(),
                GeneratedTests = user.GeneratedTests.Where(t => t.Score != null).ToList()
            });
        }
        public ActionResult My()
        {
            return RedirectToAction("Details", new {id = user.Id});
        }
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> List()
        {
            var users = await db.Users.ToListAsync();
            return View(new ProfileListViewModel() { UserList = users });
        }
    }
}