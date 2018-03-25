using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using messageBoard.Models;

namespace messageBoard.Models.ViewModel
{
    public class MemberRegisterView
    {
        public Members newMember { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage ="請輸入密碼")]
        public string Password { get; set; }


        [DisplayName("確認密碼")]
        [Compare("Password",ErrorMessage ="密碼與確認密碼不符")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        public string PasswordCheck { get; set; }
    }
}