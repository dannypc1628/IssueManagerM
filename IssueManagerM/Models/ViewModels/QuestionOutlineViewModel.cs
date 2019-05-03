using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueManagerM.Models.ViewModels
{
    public class QuestionOutlineViewModel
    {
        public int QID { get; set; }

        [Display(Name = "問題")]
        public string Title { get; set; }

        [Display(Name = "建立時間")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "狀態")]
        public string State { get; set; }

    }
}