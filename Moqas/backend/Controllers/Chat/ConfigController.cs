using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Authentication;
using Moqas.Model.Data;
using Moqas.Service.Chat;

namespace Moqas.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        MoqasContext _context;
        public ConfigController(MoqasContext context)
        {
            _context = context;
        }

        [HttpGet("config-register")]
        public async Task<IActionResult> ConfigRegister(ConfigRegister request)
        {
            return await ConfigService.ConfigRegister(this, _context, request);
        }

        [HttpGet("get-config")]
        public async Task<IActionResult> GetConfig(string browserToken)
        {
            return await ConfigService.GetConfig(this, _context, browserToken);
        }
    }
}
