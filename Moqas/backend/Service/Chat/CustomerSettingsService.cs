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
    }
}
