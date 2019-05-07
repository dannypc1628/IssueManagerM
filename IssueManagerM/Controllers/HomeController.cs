using System;
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
        public ActionResult DoList(string Mode)
        {
           
            int State = 1;
            switch (Session["UseRole"])
            {
                case "FB長":
                    State = 2;
                    break;
                case "組長":
                    State = 3;
                    break;
                case "科長":
                    State = 4;
                    break;
                case "科員":
                    State = 5;
                    break;
            }
            List<QuestionOutlineViewModel> data;
            if (Mode == "ToDo")
            {
                data = (from q in db.Question.Include(q => q.QuestionStepResult)
                                                       where q.StateID == State
                                                       select new QuestionOutlineViewModel
                                                       {
                                                           QID = q.QuestionID,
                                                           Title = q.Title,
                                                           CreateDate = q.QuestionStepResult.OrderBy(d => d.CreateDate).FirstOrDefault().CreateDate,
                                                           State = q.State.StateName
                                                       }).ToList();
                ViewData.Model = data;
            }
            if(Mode == "Doing")
            {
                data = (from q in db.Question.Include(q => q.QuestionStepResult)
                        where q.StateID > State
                        select new QuestionOutlineViewModel
                        {
                            QID = q.QuestionID,
                            Title = q.Title,
                            CreateDate = q.QuestionStepResult.OrderBy(d => d.CreateDate).FirstOrDefault().CreateDate,
                            State = q.State.StateName
                        }).ToList();  
                ViewData.Model = data;
            }

            ViewBag.Mode = Mode;
            return PartialView("List");
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
            
            List<SelectListItem> units = (from u in db.Unit
                                where u.Unit1.FirstOrDefault().UnitID == u.Unit2.FirstOrDefault().UnitID
                                select new SelectListItem {Text=u.UnitName,Value=u.UnitID.ToString() }).ToList();

            QuestionDetailViewModel data = new QuestionDetailViewModel
            {
                Question = question,
                QuestionStepResults = questionStepResults,
                UnitList =units
            };


            return View(data);
        }
        [HttpPost]
        public ActionResult QuestionDetail(int QID,int UnitID,string Content)
        {
            string UnitName = db.Unit.Find(UnitID).UnitName;
            Question question = db.Question.Find(QID);
            question.StateID = question.StateID + 1;
            question.CaseUnit = UnitID;

            QuestionStepResult result = new QuestionStepResult
            {
                QuestionID = QID,
                
                CreateDate = DateTime.Now,
                Content = "指派至" + UnitName + " " + Content,
                CreateUser = HttpContext.User.Identity.Name,
                ActionID = 2,

            };
            db.QuestionStepResult.Add(result);
            db.SaveChanges();
            return RedirectToAction("Index");
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