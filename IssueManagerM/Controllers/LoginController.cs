using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IssueManagerM.Models;
using IssueManagerM.Models.ViewModels;
using System.Data.Entity.Validation;
using System.Text;

namespace IssueManagerM.Controllers
{
    public class LoginController : Controller
    {
        private IssueManagerEntities1 db = new IssueManagerEntities1();
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
                    var UserHadRole = (from role in db.Role.Include(u => u.User)
                                       where role.User.Any(x => x.UserID == LoginData.UserID)
                                       select role).ToList();
                    //var UserHadRole = from u in db.User.Include(u => u.Role)
                    //                  orderby u.Role.FirstOrDefault().RoleID
                    //                  select u;
                    var UserUnitID = UserHadRole.FirstOrDefault().User.FirstOrDefault().UnitID;
                    Session["UseRole"] = UserHadRole.FirstOrDefault().RoleName;
                    string UserData = "" + UserUnitID+";";
                    foreach (var a in UserHadRole)
                    {
                        var r = a.RoleName;
                        if (r !=null)
                            UserData +=r + ",";
                    }
                    if (UserData.EndsWith(","))
                    {
                        UserData = UserData.Remove(UserData.Length-1);
                    }

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                        1, LoginData.UserID, DateTime.Now, DateTime.Now.AddMinutes(30), true, UserData);

                    string EncryTicket = FormsAuthentication.Encrypt(Ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, EncryTicket));
                                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("ErrorMessage", "帳號或密碼錯誤");
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
                Role role = db.Role.Find(6);
                if (data.Count() == 0)
                {
                    User user = new User()
                    {
                        UserID = SignUpData.UserID,
                        UserName = SignUpData.Name,
                        Password = SignUpData.Password,
                        Phone = SignUpData.Phone,
                        Email = SignUpData.Email,
                        
                    };
                    user.Role.Add(role);
                    db.User.Add(user);
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