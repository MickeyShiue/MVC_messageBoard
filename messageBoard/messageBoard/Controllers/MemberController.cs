using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using messageBoard.Service;
using messageBoard.Models.ViewModel;
using System.Web.Security;

namespace messageBoard.Controllers
{
    public class MemberController : Controller
    {
        private MemberService memberService;
        private MailService mailService;

        public MemberController()
        {
            memberService = new MemberService();
            mailService = new MailService();
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Guestbook");//以登入則導向

            return View();
        }
        
        public ActionResult Register(MemberRegisterView registerView)
        {
            if (ModelState.IsValid)
            {
                registerView.newMember.Password = registerView.Password;
                string AuthCode = mailService.GetVaildCode();
                registerView.newMember.AuthCode = AuthCode;
                memberService.Register(registerView.newMember);
                string Url = string.Format("http://localhost:57952/Member/EmailValidate?UserName={0}&AuthCode={1}", registerView.newMember.Account, AuthCode);
                mailService.SendRegisterMail(Url, registerView.newMember.Email);
                TempData["RegisterSatae"] = "註冊成功，請去收Emial驗證信";
                return RedirectToAction("RegisterResult");
            }

            return View(registerView);
        }

        public ActionResult RegisterResult()
        {
            return View();
        }

        public JsonResult AccountCheck(MemberRegisterView registerMember)
        {
            return Json(memberService.AccountCheck(registerMember.newMember.AuthCode), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailValidate(string UserName, string AuthCode)
        {
            ViewData["EmailValidate"] = memberService.EmailVaildate(UserName, AuthCode);
            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Guestbook");

            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberLoginView LoginMember)
        {
            string ValidStr = memberService.LoginCheck(LoginMember.UserName, LoginMember.Password);

            if (string.IsNullOrEmpty(ValidStr))
            {
                string RoleData = memberService.GetRole(LoginMember.UserName);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    LoginMember.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    RoleData,
                    FormsAuthentication.FormsCookiePath);

                string enTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));

                return RedirectToAction("Index", "Guestbook");
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChnagePasswrodView changeData)
        {
            if (ModelState.IsValid)
            {
                ViewData["ChangeState"] = memberService.ChangePassword(User.Identity.Name, changeData);
            }

            return View();
        }
    }
}