using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueManagerM.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "必須輸入使用者帳號")]
        [Display(Name ="帳號")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "必須輸入密碼")]
        [Display(Name ="密碼")]
        public string Password { get; set; }
    }
}