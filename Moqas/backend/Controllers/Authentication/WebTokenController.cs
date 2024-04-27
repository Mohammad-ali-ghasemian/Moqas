using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Moqas.Service.Authentication;

namespace Moqas.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebTokenController : ControllerBase
    {
        MoqasContext _context;
        public WebTokenController(MoqasContext context)
        {
            _context = context;
        }

        [HttpGet("get-browser-token")]
        public async Task<IActionResult> GetBrowserToken(int customerId)
        {
            return await WebTokenService.GetBrowserToken(this, _context, customerId);
        }

        [HttpGet("get-browser-token-expires")]
        public async Task<IActionResult> GetBrowserTokenExpires(int customerId)
        {
            return await WebTokenService.GetBrowserTokenExpires(this, _context, customerId);
        }

        [HttpGet("get-browser-data")]
        public async Task<IActionResult> GetBrowserData(int customerId)
        {
            return await WebTokenService.GetBrowserData(this, _context, customerId);
        }

        [HttpPut("extend-browser-token-expire-datetime")]
        public async Task<IActionResult> ExtendBrowserTokenExpireDateTime(int customerId, int extendDays)
        {
            return await WebTokenService.ExtendBrowserTokenExpireDateTime(this, _context, customerId, extendDays);
        }

        [HttpPut("delete-browser-token")]
        public async Task<IActionResult> DeleteBrowserToken(int customerId)
        {
            return await WebTokenService.DeleteBrowserToken(this, _context, customerId);
        }


        [HttpGet("get-verification-token")]
        public async Task<IActionResult> GetVerificationToken(string email)
        {
            return await WebTokenService.GetVerificationToken(this, _context, email);
        }

        [HttpGet("get-resetPassword-token")]
        public async Task<IActionResult> GetResetPasswordToken(string email)
        {
            return await WebTokenService.GetResetPasswordToken(this, _context, email);
        }
    }
}
