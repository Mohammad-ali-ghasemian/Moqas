using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Moqas.Model.Settings;
using static Moqas.Model.Data.CustomerSettings;

namespace Moqas.Service.Chat
{
    public class CustomerSettingsService
    {
        public enum TypeStyle
        {
            FAQ,
            UI,
            TOUR,
            EMAIL
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

            var setting = context.CustomerSettings.Where(u => u.CustomerId == customerId && u.Type == type).ToList();
            if (setting == null)
            {
                return controller.BadRequest("There is no such customer with this type!");
            }

            CustomerSettings[] customerSettings = new CustomerSettings[setting.Count];
            int i = 0;
            foreach(CustomerSettings item in setting)
            {
                customerSettings[i] = new CustomerSettings();
                customerSettings[i].Id = item.Id;
                customerSettings[i].Type = item.Type;
                customerSettings[i].Key = item.Key;
                customerSettings[i].Value = item.Value;
                customerSettings[i].CustomerId = item.CustomerId;
                ++i;
            }
            return controller.Ok(customerSettings);
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

        public async static Task<IActionResult> UpdateKeyValue(ControllerBase controller, MoqasContext context, int id, UpdateKeyValue newKeyValue)
        {
            var setting = context.CustomerSettings.FirstOrDefault(u => u.Id == id);
            if (setting == null)
            {
                return controller.BadRequest("There is no such id");
            }

            setting.Key = newKeyValue.Key;
            setting.Value = newKeyValue.Value;
            context.CustomerSettings.Update(setting);
            context.SaveChanges();
            return controller.Ok("New Key-Value Apllied!");
        }

        public async static Task<IActionResult> DeleteSetting(ControllerBase controller, MoqasContext context, int id)
        {
            var setting = context.CustomerSettings.FirstOrDefault(u => u.Id == id);
            if (setting == null)
            {
                return controller.BadRequest("There is no such id");
            }

            context.CustomerSettings.Remove(setting);
            context.SaveChanges();
            return controller.Ok("Setting Deleted!");
        }

    }
}
