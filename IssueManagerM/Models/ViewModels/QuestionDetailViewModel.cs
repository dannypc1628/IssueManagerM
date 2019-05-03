using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueManagerM.Models.ViewModels
{
    public class QuestionDetailViewModel
    {
        public Question Question { get; set; }

        public List<QuestionStepResult> QuestionStepResults { get; set; }
    }
}