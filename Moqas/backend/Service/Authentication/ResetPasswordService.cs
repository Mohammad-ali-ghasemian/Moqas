using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model.Authentication;
using Moqas.Model.Data;

namespace Moqas.Service.Authentication
{
    public class ResetPasswordService
    {
        public static async Task<IActionResult> ResetPasswordProcess(ControllerBase controller, MoqasContext context, ResetPasswordRequest request)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.PasswordResetToken == request.ResetPasswordToken);
            if (customer == null || customer.ResetTokenExpires < DateTime.Now)
            {
                return controller.BadRequest("Invalid Token!");
            }

            CustomerRegisterService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            customer.PasswordResetToken = null;
            customer.ResetTokenExpires = null;

            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            return controller.Ok("Password has been reset!");
        }
    }
}
