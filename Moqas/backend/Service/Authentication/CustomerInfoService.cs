using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;

namespace Moqas.Service.Authentication
{
    public class CustomerInfoService
    {

        public async static Task<IActionResult> GetCustomer(ControllerBase controller, MoqasContext context, bool token0Email1, string tokenEmail)
        {
            if (!token0Email1)
            {
                return GetCustomerByToken(controller, context, tokenEmail);
            }
            else
            {
                return GetCustomerByEmail(controller, context, tokenEmail);
            }
        }



        public static IActionResult GetCustomerByToken(ControllerBase controller, MoqasContext context, string token)
        {
            var customer = context.Customers.FirstOrDefault(u => u.BrowserToken == token);
            if (customer == null)
            {
                return controller.BadRequest("There is no customer with this token!");
            }

            return controller.Ok(customer);
        }



        public static IActionResult GetCustomerByEmail(ControllerBase controller, MoqasContext context, string email)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Email == email);
            if (customer == null)
            {
                return controller.BadRequest("There is no customer with this email!");
            }

            return controller.Ok(customer);
        }
    }
}
