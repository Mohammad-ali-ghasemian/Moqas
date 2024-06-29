using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model.Authentication;
using Moqas.Model.Data;
using System.Security.Cryptography;

namespace Moqas.Service.Authentication
{
    public class CustomerLoginService
    {


        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }



        public static async Task<IActionResult> LoginRequestProcess(ControllerBase controller, MoqasContext context, CustomerLogin request)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (customer == null)
            {
                return controller.BadRequest("Customer Not Found!");
            }

            if (!VerifyPasswordHash(request.Password, customer.PasswordHash, customer.PasswordSalt))
            {
                return controller.BadRequest("Password Is Incorrect!");
            }

            if (customer.VerifiedAt == null)
            {
                return controller.BadRequest("Customer Not Verified!");
            }

            customer.BrowserToken = CustomerRegisterService.CreateToken(16);
            customer.BrowserTokenExpires = DateTime.Now.AddDays(4);

            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            return controller.Ok(customer.Id + " " + customer.BrowserToken);
        }
    }
}
