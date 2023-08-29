using Microsoft.AspNetCore.Mvc;
using Moqas.Model;
using Moqas.Model.Authentication;
using Moqas.Model.Data;
using System.Security.Cryptography;

namespace Moqas.Service.Authentication
{
    public class CustomerRegisterService
    {
        public static async Task<IActionResult> RegisterRequestProcess(ControllerBase controller, CustomerContext context, CustomerRegister request)
        {
            if (CheckDuplicateEmail(context, request))
            {
                return controller.BadRequest("Customer Already Exists!");
            }
            CreateCustomer(context, request.Email, request.Password);
            EmailService.SendVerificationEmail(request.Email, context.Customers.FirstOrDefault(u => u.Email == request.Email).VerificationToken, "activation");
            return controller.Ok("Customer Successfully Created!");
        }

        public static bool CheckDuplicateEmail(CustomerContext context, CustomerRegister request)
        {
            return context.Customers.Any(u => u.Email == request.Email);
        }

        public static async void CreateCustomer(CustomerContext context, string email, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var customer = new Customer
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateToken(4),
                BrowserToken = CreateToken(16)
            };

            context.Customers.Add(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static string CreateToken(int n)
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(n));
        }

    }
}
