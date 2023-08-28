using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model;
using Moqas.Model.Data;
using System.Security.Cryptography;

namespace Moqas.Service
{
    public class CustomerRegisterService
    {
        public static async Task<IActionResult> RegisterRequestProcess(ControllerBase controller, CustomerContext context, CustomerRegister request)
        {
            if (CheckEmail(context, request))
            {
                return controller.BadRequest("Customer Already Exists!");
            }
            CreateCustomer(context, request.Email, request.Password);
            return controller.Ok("Customer Successfully Created!");
        }

        public static bool CheckEmail(CustomerContext context, CustomerRegister request)
        {
            return context.Customers.Any(u => u.Email == request.Email);
        }

        public static async void CreateCustomer(CustomerContext context, string email, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var Customer = new Customer
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateToken()
            };

            context.Customers.Add(Customer);
            await context.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static string CreateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }

    }
}
