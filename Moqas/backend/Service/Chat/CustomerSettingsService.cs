using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Chat;
using Moqas.Model.Data;
using static Moqas.Model.Data.CustomerSettings;

namespace Moqas.Service.Chat
{
    public class CustomerSettingsService
    {
        public enum TypeStyle
        {
            FAQ,
            UI,
            TOUR
        }

        public async static Task<IActionResult> InsertSetting(ControllerBase controller, MoqasContext context, GetCustomerSettings settings)
        {
            if (context.Customers.FirstOrDefault(u => u.Id == settings.CustomerId) == null)
            {
                return controller.BadRequest("There is no such customer id!");
            }
            if (!Enum.IsDefined(typeof(TypeStyle), settings.Type))
            {
                return controller.BadRequest("Invalid Type");
            }
            var customerSettings = new CustomerSettings
            {
                CustomerId = settings.CustomerId,
                Type = settings.Type,
                Key = settings.Key,
                Value = settings.Value,
            };
            context.CustomerSettings.Add(customerSettings);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok("Setting Inserted Succesfuly");
        }


        public async static Task<IActionResult> GetSetting(ControllerBase controller, MoqasContext context, int customerId, string type)
        {
            if (context.Customers.FirstOrDefault(u => u.Id == customerId) == null)
            {
                return controller.BadRequest("There is no such customer id!");
            }
            if (!Enum.IsDefined(typeof(TypeStyle), type))
            {
                return controller.BadRequest("Invalid Type");
            }

            var setting = context.CustomerSettings.Where(u => u.CustomerId == customerId && u.Type == type).FirstOrDefault();
            if (setting == null)
            {
                return controller.BadRequest("There is no such customer with this type!");
            }
            return controller.Ok(new CustomerSettings
            {
                Id = setting.Id,
                Type = setting.Type,
                Key = setting.Key,
                Value = setting.Value,
                CustomerId = setting.CustomerId,
            });
        }

        public async static Task<IActionResult> UpdateType(ControllerBase controller, MoqasContext context, int id, string type)
        {
            var setting = context.CustomerSettings.FirstOrDefault(u => u.Id == id);
            if (setting == null)
            {
                return controller.BadRequest("There is no such id");
            }
            if (!Enum.IsDefined(typeof(TypeStyle), type))
            {
                return controller.BadRequest("Invalid Type");
            }

            setting.Type = type;
            context.CustomerSettings.Update(setting);
            context.SaveChanges();
            return controller.Ok("New Type Apllied!");
        }
    }
}
