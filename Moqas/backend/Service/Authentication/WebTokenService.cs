using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Authentication;
using Moqas.Model.Data;

namespace Moqas.Service.Authentication
{
    public class WebTokenService
    {


        public async static Task<IActionResult> GetBrowserToken(ControllerBase controller, MoqasContext context, int customerId)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);

            if (customer == null)
            {
                return controller.BadRequest("Customer Not Found!");
            }

            return controller.Ok(customer.BrowserToken);
        }



        public async static Task<IActionResult> GetBrowserTokenExpires(ControllerBase controller, MoqasContext context, int customerId)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);

            if (customer == null)
            {
                return controller.BadRequest("Customer Not Found!");
            }

            return controller.Ok(customer.BrowserTokenExpires);
        }



        public async static Task<IActionResult> GetBrowserData(ControllerBase controller, MoqasContext context, int customerId)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);

            if (customer == null)
            {
                return controller.BadRequest("Customer Not Found!");
            }

            var browserData = new CustomerToken
            {
                customerId = customerId,
                BrowserToken = customer.BrowserToken,
                BrowserTokenExpires = customer.BrowserTokenExpires
            };

            return controller.Ok(browserData);
        }



        public async static Task<IActionResult> ExtendBrowserTokenExpireDateTime(ControllerBase controller, MoqasContext context, int customerId, int extendDays)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);

            if (customer == null)
            {
                return controller.BadRequest("Customer Not Found!");
            }

            if (extendDays > 4)
            {
                return controller.BadRequest("Extend Days Cannot Be More Than 4 Days!");
            }

            customer.BrowserTokenExpires = DateTime.Now.AddDays(extendDays);
            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            return controller.Ok("Token Expires DateTime Updated!");
        }



        public async static Task<IActionResult> DeleteBrowserToken(ControllerBase controller, MoqasContext context, int customerId)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);

            if(customer == null)
            {
                return controller.BadRequest("Customer Not Found!");
            }

            customer.BrowserToken = null;
            customer.BrowserTokenExpires = null;
            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            return controller.Ok("Browser Token And Expire DateTime Deleted!");
        }

    }
}
