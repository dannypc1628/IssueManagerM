using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IssueManagerM.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="必須輸入身分證字號")]
        [Display(Name="身份證字號")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "姓名不能空白")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage ="密碼不能空白")]
        [Display(Name="密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "密碼不能空白")]
        [Display(Name ="再輸入一次密碼")]
        public string SecondPassword { get; set; }

        [Required(ErrorMessage = "電話不能空白")]
        [Display(Name="電話")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "電子信箱不能空白")]
        [Display(Name="電子信箱")]
        public string Email { get; set; }
    }
}