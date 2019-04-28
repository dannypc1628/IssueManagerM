using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueManagerM.Models;
using IssueManagerM.Models.ViewModels;

namespace IssueManagerM.Controllers
{
    public class LoginController : Controller
    {
        private IssueManagerEntities db = new IssueManagerEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel LoginData)
        {
            if (ModelState.IsValid)
            {
                var data = from user in db.User
                           where user.UserID == LoginData.UserID && user.Password == LoginData.Password
                           select user;
                if (data.Count() == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel SignUpData)
        {
            if (ModelState.IsValid)
            {
                var data = from user in db.User
                           where user.UserID == SignUpData.UserID
                           select user.UserID;
                if (data.Count() == 0)
                {
                    db.User.Add(new User()
                    {
                        UserID = SignUpData.UserID,
                        UserName = SignUpData.Name,
                        Password = SignUpData.Password,
                        Phone = SignUpData.Phone,
                        Email = SignUpData.Email
                    });
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UserID", "該帳號已註冊！");
                }
            }
            return View();
        }
    }
}