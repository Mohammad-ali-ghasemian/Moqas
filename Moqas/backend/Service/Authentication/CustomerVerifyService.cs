using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model.Data;

namespace Moqas.Service.Authentication
{
    public class CustomerVerifyService
    {
        public static async Task<IActionResult> VerifyRequestProcess(ControllerBase controller, CustomerContext context, string token)
        {
            var customer = context.Customers.FirstOrDefault(u => u.VerificationToken == token);
            if (customer == null)
            {
                return controller.BadRequest("Invalid Token!");
            }
            customer.VerifiedAt = DateTime.Now;
            context.SaveChangesAsync();

            return controller.Ok("Customer Verified!");
        }
    }
}
