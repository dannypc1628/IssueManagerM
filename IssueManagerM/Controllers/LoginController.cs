using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
                int UserCount = (from user in db.User
                                 where user.UserID == LoginData.UserID && user.Password == LoginData.Password
                                 select  user.UserName).Count();
                if (UserCount == 1)
                {
                    List<int> UserHadRole = (from role in db.Role
                                            where role.User.Any(x=>x.UserID== LoginData.UserID)
                                            select role.RoleID).ToList();
                    string UserRole = "User";
                    foreach(int a in UserHadRole)
                    {
                        UserRole += ","+a;
                    }
                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                        1, LoginData.UserID, DateTime.Now, DateTime.Now.AddMinutes(30), true, UserRole);

                    string EncryTicket = FormsAuthentication.Encrypt(Ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, EncryTicket));
                    

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

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}