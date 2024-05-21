using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Moqas.Service.Authentication
{
    public class EmailService
    {
        public static void SendVerificationEmail(string Email, string token, string reason)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("MoqasSupport@moqas-chat.ir"));
            email.To.Add(MailboxAddress.Parse(Email));
            email.Subject = "Do Not Reply";
            email.Body = new TextPart(TextFormat.Html) { Text = $"Your {reason} code is :<br/><b>{token}</b>" };

            using var smtp = new SmtpClient();
            smtp.Connect("mail.moqas-chat.ir", 25, SecureSocketOptions.StartTls);
            smtp.Authenticate("lesley.volkman@ethereal.email", "pg9AC93rFvytpAjbZ1");
            smtp.Send(email);
            smtp.Disconnect(true);
  
        }
    }
}
