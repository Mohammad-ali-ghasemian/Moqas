using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Chat;
using Moqas.Model.Data;

namespace Moqas.Service.Chat
{
    public class CustomerSettingsService
    {
        public async static Task<IActionResult> InsertSetting(ControllerBase controller, MoqasContext context, CustomerSettings settings)
        {
            if (context.Customers.FirstOrDefault(u => u.Id == settings.CustomerId) == null)
            {
                return controller.BadRequest("There is no such customer id!");
            }
            context.CustomerSettings.Add(settings);
            try
            {
                context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok(settings.Id);
        }
    }
}
