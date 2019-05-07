using IssueManagerM.Models;
using IssueManagerM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagerM.Controllers
{
    public class UserController : Controller
    {
        private IssueManagerEntities1 db = new IssueManagerEntities1();
        // GET: User
        public ActionResult ChangeRole()
        {
            string UserID = HttpContext.User.Identity.Name;
            List<Role> data = (from r in db.Role
                       where r.User.Any(c => c.UserID == UserID)
                       select r).ToList();

            return View(data);
        }

        [HttpPost]
        public ActionResult ChangeRole(int RoleID)
        {
            string UserID = HttpContext.User.Identity.Name;
            Role data = (from r in db.Role
                               where r.User.Any(c => c.UserID == UserID) && r.RoleID==RoleID
                               select r).Single();
            Session["UseRole"] = data.RoleName;
            return RedirectToAction("Index", "Home");
        }
    }
}