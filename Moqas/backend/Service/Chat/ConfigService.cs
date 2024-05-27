using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Authentication;
using Moqas.Model.Data;

namespace Moqas.Service.Chat
{
    public class ConfigService
    {
        public async static Task<IActionResult> ConfigRegister(ControllerBase controller, MoqasContext context, ConfigRegister request)
        {
            var customer = context.Customers.FirstOrDefault(u => u.BrowserToken == request.BrowserToken);
            if (customer == null)
            {
                return controller.BadRequest("There is no such Browser Token!");
            }
        }
    }
}
