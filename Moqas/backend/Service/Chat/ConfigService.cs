using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Chat;
using Moqas.Model.Data;
using Moqas.Service.Authentication;

namespace Moqas.Service.Chat
{
    public class ConfigService
    {
        public async static Task<IActionResult> ConfigRegister(ControllerBase controller, MoqasContext context, ConfigRegister request)
        {
            if (context.Customers.FirstOrDefault(u => u.ConfigUsername == request.Username) != null)
            {
                return controller.BadRequest("The Username you have chosen for config is already exists!");
            }
            var customer = context.Customers.FirstOrDefault(u => u.BrowserToken == request.BrowserToken);
            if (customer == null)
            {
                return controller.BadRequest("There is no such Browser Token!");
            }
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                return controller.BadRequest("Confirmed Password Not Equall to the Password!");
            }

            CustomerRegisterService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.ConfigUsername = request.Username;
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            customer.ConfigCreatedAt = DateTime.Now;
            customer.ConfigExpires = DateTime.Now.AddDays(30);

            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            return controller.Ok(customer.Id);
        }

        public async static Task<IActionResult> GetConfig(ControllerBase controller, MoqasContext context, string browserToken)
        {
            var customer = context.Customers.FirstOrDefault(u => u.BrowserToken == browserToken);
            if (customer == null)
            {
                return controller.BadRequest("There is no such Browser Token!");
            }
            return controller.Ok(new ConfigData
            {
                Id = customer.Id,
                Email = customer.Email,
                WebsiteLink = customer.WebsiteLink,
                BrowserToken = customer.BrowserToken,
                ConfigUsername = customer.ConfigUsername,
                ConfigCreatedAt = customer.ConfigCreatedAt,
                ConfigExpires = customer.ConfigExpires,
            });
        }
    }
}
