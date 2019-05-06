using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagerM.Models.ViewModels
{
    public class QuestionDetailViewModel
    {
        public Question Question { get; set; }

        public List<QuestionStepResult> QuestionStepResults { get; set; }

        public List<SelectListItem> UnitList { get; set; }

        public List<SelectListItem> Officers { get; set; }

    }
}