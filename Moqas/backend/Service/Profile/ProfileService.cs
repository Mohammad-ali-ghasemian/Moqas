using Moqas.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moqas.Service.Authentication;

namespace Moqas.Service.Profile
{
    public class ProfileService
    {
        public async static Task<Model.Profile.Profile> GetProfile(MoqasContext context, int customerId)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.Id == customerId);

            var profile = new Model.Profile.Profile
            {
                CustomerId = customerId,
                Email = customer.Email,
                VerifiedAt = customer.VerifiedAt
            };
            return profile;
        }

        public async static Task<IActionResult> UpdateEmail(ControllerBase controller, MoqasContext context, int customerId, string email)
        {
            if( CustomerRegisterService.CheckDuplicateEmail(context, new Model.Authentication.CustomerRegister { Email = email }) ){
                return controller.BadRequest("Customer Already Exists!");
            }
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.Id == customerId);
            customer.VerificationToken = CustomerRegisterService.CreateToken(4);
            context.Customers.Update(customer);
            context.SaveChanges();

            EmailService.SendVerificationEmail(email, customer.VerificationToken, "verification");
            return controller.Ok("Verification Email Sent Successfully!");
        }

        public static async Task<IActionResult> VerifyRequestProcess(ControllerBase controller, MoqasContext context, string token, string email)
        {
            var customer = context.Customers.FirstOrDefault(u => u.VerificationToken == token);
            if (customer == null)
            {
                return controller.BadRequest("Invalid Token!");
            }
            customer.Email = email;
            context.Customers.Update(customer);
            context.SaveChangesAsync();

            return controller.Ok("New Email Verified!");
        }

        public static async Task<IActionResult> UpdatePassword(ControllerBase controller, MoqasContext context, int id, string password)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == id);

            CustomerRegisterService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            context.Customers.Update(customer);
            context.SaveChangesAsync();

            return controller.Ok("New Password Set!");
        }
    }
}
