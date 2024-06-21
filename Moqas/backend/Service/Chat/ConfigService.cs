using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Chat;
using Moqas.Model.Data;
using Moqas.Service.Authentication;
using System.Collections;

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
            if (customer.VerifiedAt == null)
            {
                return controller.BadRequest("Customer Not Verified!");
            }
            if (customer.ConfigExpires > DateTime.Now)
            {
                return controller.BadRequest("Your Last Config has not Expired Yet!");
            }

            CustomerRegisterService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.ConfigUsername = request.Username;
            customer.ConfigPasswordHash = passwordHash;
            customer.ConfigPasswordSalt = passwordSalt;
            customer.ConfigCreatedAt = DateTime.Now;
            customer.ConfigExpires = DateTime.Now.AddDays(30);

            context.Customers.Update(customer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }

            InsertDefaultCustomerSettings(context, customer.Id);

            return controller.Ok(customer.Id);
        }

        public async static void InsertDefaultCustomerSettings(MoqasContext context, int customerId)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("icon_backgroundColor", "#8e44ad");
            hashtable.Add("icon", "CommentOutlined");
            hashtable.Add("iconColor", "#ecf0f1");
            hashtable.Add("size", "medium");
            hashtable.Add("position", "left");
            hashtable.Add("shape", "circle");
            hashtable.Add("title", "");
            hashtable.Add("status", "");
            hashtable.Add("intro", "");
            hashtable.Add("showFaqFirst", "true");
            hashtable.Add("requireUsername", "false");
            hashtable.Add("requirePhone", "false");
            hashtable.Add("chat_background", "#ecf0f1");
            ICollection keys = hashtable.Keys;

            foreach(string item in keys)
            {
                context.CustomerSettings.Add(new CustomerSettings
                {
                    CustomerId = customerId,
                    Type = "UI",
                    Key = item,
                    Value = (string) hashtable[item]
                });
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
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
