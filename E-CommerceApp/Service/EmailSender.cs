using E_CommerceApp.Interfaces;
using System.Net;
using System.Net.Mail;

namespace E_CommerceApp.Service
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            string mail = "admin@gmail.com";
            string pw = "4343";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            return client.SendMailAsync(
                new MailMessage(from: mail, to: email, subject, message));
        }
    }
}
