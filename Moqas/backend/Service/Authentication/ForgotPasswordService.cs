using Moqas.Model.Data;
using Moqas.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Moqas.Service.Authentication
{
    public class ForgotPasswordService
    {
        public static async Task<IActionResult> ForgotPasswordEmail(ControllerBase controller, CustomerContext context, string email)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.Email == email);
            if (customer == null)
            {
                return controller.BadRequest("Customer does not exists!");
            }

            customer.PasswordResetToken = CustomerRegisterService.CreateToken(4);
            customer.ResetTokenExpires = DateTime.Now.AddHours(1);
            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            EmailService.SendVerificationEmail(email, customer.PasswordResetToken, "Reset Password");

            return controller.Ok("Reset Password Token Sent!");
        }
    }
}
