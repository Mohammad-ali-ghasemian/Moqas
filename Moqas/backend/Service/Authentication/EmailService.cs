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
            {
                //smtp.Connect("webmail.moqas-chat.ir", 587, SecureSocketOptions.StartTls);

                //smtp.Connect("webmail.moqas-chat.ir", 25, SecureSocketOptions.StartTls);
                smtp.Connect("webmail.moqas-chat.ir", 587, false);
                smtp.Authenticate("MoqasSupport@moqas-chat.ir", "fF#90a54c");
                smtp.Send(email);
                smtp.Disconnect(true);
                
            }

            /*var client = new SmtpClient("smtp.yourserver.com")
            {
                Port = 25, // Use the port for SMTP without encryption
                EnableSsl = false, // Disable SSL/TLS
                Credentials = new NetworkCredential("yourusername", "yourpassword")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("you@yourdomain.com"),
                Subject = "Test Email",
                Body = "This is a test email",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("recipient@theirdomain.com");

            try
            {
                client.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }*/


            return controller.Ok("Email sent succesfully!");
        }
    }
}
