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
        [StringLength(10,ErrorMessage ="身份證字號應該是10個字元",MinimumLength =10)]
        [RegularExpression(@"^[A-Z]\d{9}$",ErrorMessage ="開頭為大寫英文字元一個後面接9個數字")]
        [Display(Name="身份證字號")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "姓名不能空白")]
        [Display(Name = "姓名")]
        [StringLength(10, ErrorMessage = "姓名應該是2-10個字元", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage ="密碼不能空白")]
        [Display(Name="密碼")]
        [StringLength(10, ErrorMessage = "密碼長度需要是6-10個字元", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "密碼不能空白")]
        [Display(Name ="再輸入一次密碼")]
        [Compare("Password",ErrorMessage ="兩次密碼不相同")]
        public string SecondPassword { get; set; }

        [Required(ErrorMessage = "電話不能空白")]
        [Display(Name="電話")]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "電話只能8到10位數之數字")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "電子信箱不能空白")]
        [Display(Name="電子信箱")]
        [EmailAddress(ErrorMessage ="這不是E-mail格式")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}