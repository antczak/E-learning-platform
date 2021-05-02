using E_learning_platform.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize]
    public class AjaxController : BaseController
    {
        public ActionResult Index()
        {
            return HttpNotFound();
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpPost]
        public JsonResult Order(AjaxOrder model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<IOrderable> workplace = null;
                switch (model.Tab)
                {
                    case "presentations": workplace = db.Presentations; break;
                    case "videos": workplace = db.Videos; break;
                    case "documents": workplace = db.Documents; break;
                    case "files": workplace = db.Files; break;
                    case "tests": workplace = db.Tests; break;
                }
                foreach (var item in model.Order)
                {
                    var orderable = workplace.Where(o => o.Id == item.Id).FirstOrDefault();
                    orderable.Order = item.Order;
                    Debug.WriteLine(item.Id + " => " + item.Order);
                }
                db.SaveChanges();
                return Json(new AjaxResult() { IsOk = true });
            }
            return Json(new AjaxResult() { IsOk = false });
        }
    }
}