using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;

namespace Moqas.Service.Authentication
{
    public class EmailService
    {
        public async static Task<IActionResult> SendEmail(ControllerBase controller, MoqasContext context, string Email, byte emailType)
        {
            string reason = string.Empty;
            string token = string.Empty;
            switch (emailType)
            {
                case 0:
                    //verificationToken
                    reason = "activation";
                    try
                    {
                        token = context.Customers.FirstOrDefault(u => u.Email == Email).VerificationToken;
                    }catch(Exception ex) { return controller.BadRequest("There is no such Email or the email you provided has no Verification Token!"); }
                    break;
            }
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

            return controller.Ok("Email sent succesfully!");
        }
    }
}
