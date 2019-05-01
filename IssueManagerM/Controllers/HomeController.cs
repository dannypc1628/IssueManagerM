using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueManagerM.Models;
using IssueManagerM.Models.ViewModels;


namespace IssueManagerM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IssueManagerEntities db = new IssueManagerEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateQuestionViewModel CreateData)
        {
            if (ModelState.IsValid)
            {
                db.Question.Add(new Question
                {
                    Title = CreateData.Title,
                    Content = CreateData.Content,
                    Asker = CreateData.Asker,
                    AskDate = CreateData.AskData,
                    StateID = 1
                });

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}