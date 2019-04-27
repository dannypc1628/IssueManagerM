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
                return Content("正確");
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
                return Content("正確");
            }
            return View();
        }
    }
}