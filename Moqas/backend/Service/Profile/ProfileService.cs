using Moqas.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moqas.Service.Authentication;
using Moqas.Model.Profile;

namespace Moqas.Service.Profile
{
    public class ProfileService
    {
        private static Model.Profile.Profile profile = new Model.Profile.Profile();

        public async static Task<Model.Profile.Profile> GetProfile(MoqasContext context, int customerId)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.Id == customerId);

            profile.Id = customerId;
            profile.Email = customer.Email;
            profile.WebsiteLink = customer.WebsiteLink;
            profile.VerificationToken = customer.VerificationToken;
            profile.VerifiedAt = customer.VerifiedAt;
            profile.PasswordResetToken = customer.PasswordResetToken;
            profile.ResetTokenExpires = customer.ResetTokenExpires;
            profile.BrowserToken = customer.BrowserToken;
            profile.BrowserTokenExpires = customer.BrowserTokenExpires;
            profile.ConfigUsername = customer.ConfigUsername;
            profile.ConfigCreatedAt = customer.ConfigCreatedAt;
            profile.ConfigExpires = customer.ConfigExpires;
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

            return controller.Ok("New Verification Token Created Successfully!");
        }



        public static async Task<IActionResult> VerifyRequestProcess(ControllerBase controller, MoqasContext context, string verificationToken, string email)
        {
            var customer = context.Customers.FirstOrDefault(u => u.VerificationToken == verificationToken);
            if (customer == null)
            {
                return controller.BadRequest("Invalid Token!");
            }
            customer.Email = email;
            context.Customers.Update(customer);
            context.SaveChangesAsync();

            return controller.Ok("New Email Verified!");
        }



        public static async Task<IActionResult> UpdatePassword(ControllerBase controller, MoqasContext context, int customerId, string password)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);

            CustomerRegisterService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            context.Customers.Update(customer);
            context.SaveChangesAsync();

            return controller.Ok("New Password Set!");
        }
    }
}
