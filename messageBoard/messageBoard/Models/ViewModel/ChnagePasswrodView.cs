using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace messageBoard.Models.ViewModel
{
    public class ChnagePasswrodView
    {
        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入新密碼")]
        public string NewPassword { get; set; }

        [DisplayName("新密碼確認")]
        [Required(ErrorMessage = "請輸入密碼")]
        [Compare("NewPassword",ErrorMessage ="兩次密碼不一致，請確認")]
        public string NewPasswordCheck { get; set; }

    }
}