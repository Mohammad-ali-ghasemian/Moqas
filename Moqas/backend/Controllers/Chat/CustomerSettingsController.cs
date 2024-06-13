using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Chat;
using Moqas.Model.Data;
using Moqas.Service.Chat;

namespace Moqas.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSettingsController : ControllerBase
    {
        MoqasContext _context;
        public CustomerSettingsController(MoqasContext context)
        {
            _context = context;
        }

        [HttpPost("insert-setting")]
        public async Task<IActionResult> InsertSetting(GetCustomerSettings settings)
        {
            return await CustomerSettingsService.InsertSetting(this, _context, settings);
        }
    }
}
