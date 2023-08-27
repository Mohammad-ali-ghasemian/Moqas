﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model;
using Moqas.Model.Data;
using System.Security.Cryptography;

namespace Moqas.Service
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

        public static async Task<IActionResult> LoginRequestProcess(ControllerBase controller, CustomerContext context, CustomerLogin request)
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

            return controller.Ok($"Welcome Back, {customer.Email}");
        }
    }
}
