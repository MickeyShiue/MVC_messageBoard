using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using messageBoard.Models;
using messageBoard.Models.ViewModel;

namespace messageBoard.Service
{
    public class MemberService
    {
        private MyGuestbookEntities db;

        public MemberService()
        {
            db = new MyGuestbookEntities();
        }

        public void Register(Members members)
        {
            members.Password = HashPassword(members.Password);
            db.Members.Add(members);
            db.SaveChanges();
        }

        public string HashPassword(string Password)
        {
            string saltkey = "a7dasd54as65d4a56d4aasd789";
            string saltAndPassword = string.Concat(Password, saltkey);

            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashData = sha1Hasher.ComputeHash(PasswordData);

            string result = string.Empty;

            for (int i = 0; i < HashData.Length; i++)
            {
                result += HashData[i].ToString("x2");
            }
            return result;
        }

        public bool AccountCheck(string Account)
        {
            Members search = db.Members.Find(Account);
            bool result = (search == null);
            return result;
        }

        public string EmailVaildate(string UserName, string AuthCode)
        {
            Members ValidDataMember = db.Members.Find(UserName);
            string ValidateStr = string.Empty;

            if (ValidDataMember != null)
            {
                if (ValidDataMember.AuthCode == AuthCode)
                {
                    ValidDataMember.AuthCode = null;
                    db.SaveChanges();
                    ValidateStr = "帳號信箱驗證成功，現在可以登入了";
                }
                else
                {
                    ValidateStr = "驗證碼錯誤，請重新確認或再註冊";
                }
            }
            else
            {
                ValidateStr = "傳送資料錯誤，請重新確認或再註冊";
            }

            return ValidateStr;
        }

        public string LoginCheck(string Account, string Password)
        {
            Members LoginMember = db.Members.Find(Account);
            if (LoginMember != null)
            {
                if (string.IsNullOrWhiteSpace(LoginMember.AuthCode))
                {
                    if (PasswordCheck(LoginMember, Password))
                    {
                        return "";
                    }
                    else
                    {
                        return "密碼輸入錯誤";
                    }
                }
                else
                {
                    return "此帳號未經過Email驗證，請去收信";
                }


            }
            else
            {
                return "無此會員，請註冊會員";
            }
        }

        public bool PasswordCheck(Members checkmember, string Password)
        {
            bool result = checkmember.Password.Equals(HashPassword(Password));
            return result;
        }

        public string GetRole(string UserName)
        {
            string Role = "User";

            Members LoginMember = db.Members.Find(UserName);

            if (LoginMember.IsAdmin)
            {
                Role += ",Admin";
            }
            return Role;
        }

        public string ChangePassword(string UserName, ChnagePasswrodView chnagePasswrodView)
        {
            Members LoginMember = db.Members.Find(UserName);
            if (PasswordCheck(LoginMember, chnagePasswrodView.Password))
            {
                LoginMember.Password = HashPassword(chnagePasswrodView.NewPassword);
                db.SaveChanges();
                return "修改密碼成功";
            }
            else
            {
                return "修改密碼失敗";
            }
        }
    }
}