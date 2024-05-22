using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace Moqas.Service.Authentication
{
    public class EmailService
    {
        public async static Task<IActionResult> SendEmail(ControllerBase controller, MoqasContext context, string Email, byte emailType)
        {
            string reason = string.Empty;
            string? token = string.Empty;
            switch (emailType)
            {
                case 0:
                    //Verification Token
                    reason = "Your activation code is :";
                    token = context.Customers.FirstOrDefault(u => u.Email == Email).VerificationToken;
                    if (token == null)
                    {
                        return controller.BadRequest("There is no such Email or the email you provided has no Verification Token!");
                    }
                    break;

                case 1:
                    //Forgot Password Token
                    reason = "Your forgot password code is :";
                    token = context.Customers.FirstOrDefault(u => u.Email == Email).PasswordResetToken;
                    if (token == null)
                    {
                        return controller.BadRequest("There is no such Email or the email you provided has no Password Reset Token!");
                    }
                    break;

                case 2:
                    //Notify New Chat
                    reason = "The New Chat Has Started";
                    break;
            }
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("MoqasSupport@moqas-chat.ir"));
            email.To.Add(MailboxAddress.Parse(Email));
            email.Subject = "Do Not Reply";
            email.Body = new TextPart(TextFormat.Html) { Text = $"{reason}<br/><b>{token}</b>" };

            using var smtp = new SmtpClient();
            {
                try
                {
                    smtp.Connect("webmail.moqas-chat.ir", 587, false);
                    smtp.Authenticate("MoqasSupport@moqas-chat.ir", "fF#90a54c");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }catch(Exception ex) { return controller.BadRequest("There was a problem in connecting or sending the mail!"); }
            }

            return controller.Ok("Email sent succesfully!");
        }
    }
}
