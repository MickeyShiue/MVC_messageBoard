using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace messageBoard.Service
{
    public class MailService
    {

        private const string gmail_account = "XXXXXXXXX";
        private const string gmail_password = "XXXXXXXXXXX";
        private const string gmail_mail = "XXXXXXXXXXXXXXXXX";

        public string GetVaildCode()
        {
            return "A1B23C4D5E";
        }

        public void SendRegisterMail(string MailBody,string ToMail)
        {

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            smtpServer.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(gmail_mail);
            mail.To.Add(ToMail);
            mail.Subject = "會員註冊認證信";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            smtpServer.Send(mail);
        }
    }
}