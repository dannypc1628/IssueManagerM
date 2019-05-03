﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using IssueManagerM.Models;
using IssueManagerM.Models.ViewModels;


namespace IssueManagerM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IssueManagerEntities1 db = new IssueManagerEntities1();

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "1")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public ActionResult Create(CreateQuestionViewModel CreateData)
        {
            if (ModelState.IsValid)
            {
                Question QuestionItem = new Question
                {
                    Title = CreateData.Title,
                    Content = CreateData.Content,
                    Asker = CreateData.Asker,
                    AskDate = CreateData.AskData,
                    StateID = 2
                };
                db.Question.Add(QuestionItem);

                db.SaveChanges();

                QuestionStepResult DetailItem = new QuestionStepResult
                {
                    CreateDate = DateTime.Now,
                    CreateUser = User.Identity.Name,
                    CreateUserRole = 1,
                    Content = null,
                    ActionID = 1,
                    QuestionID = QuestionItem.QuestionID

                };
                db.QuestionStepResult.Add(DetailItem);
                db.SaveChanges();


                return RedirectToAction("Index");

            }

            return View();
        }
        public ActionResult DoList()
        {
            List<QuestionOutlineViewModel> data = (from q in db.Question.Include(q=>q.QuestionStepResult)
                       select new QuestionOutlineViewModel {
                           QID=q.QuestionID,
                           Title =q.Title ,
                           CreateDate = q.QuestionStepResult.OrderBy(d=>d.CreateDate).FirstOrDefault().CreateDate ,
                           State =q.State.StateName
                       }).ToList();
            ViewData.Model = data;
            return View("List");
        }
        public ActionResult QuestionDetail(int QID)
        {
            Question question = (from q in db.Question
                                where q.QuestionID == QID
                                select q).Single();
            List<QuestionStepResult> questionStepResults = (from q in db.QuestionStepResult
                                                            where q.QuestionID == QID
                                                            orderby q.CreateDate
                                                            select q).ToList();
            QuestionDetailViewModel data = new QuestionDetailViewModel
            {
                Question = question,
                QuestionStepResults = questionStepResults
            };
            return View(data);
        }
        public ActionResult DoingList()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}