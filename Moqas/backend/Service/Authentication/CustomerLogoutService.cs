using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model.Data;

namespace Moqas.Service.Authentication
{
    public class CustomerLogoutService
    {

        public static async Task<IActionResult> LogoutRequestProcess(ControllerBase controller, MoqasContext context, string browserToken)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.BrowserToken == browserToken);
            if (customer == null)
            {
                return controller.BadRequest("customer does not found!");
            }

            customer.BrowserTokenExpires = null;
            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            return controller.Ok(customer.Id);
        }
        
    }
}
