using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueManagerM.Models.ViewModels
{
    public class CreateQuestionViewModel
    {

        [Required]
        [Display(Name = "發問人名稱")]
        public string Asker { get; set; }

        [Required]
        [Display(Name ="發問時間")]
        [DataType(DataType.DateTime)]
        public DateTime AskData { get; set; }

        [Required]
        [Display(Name ="問題原文")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="補充與描述")]
        public string Content { get; set; }

    }
}