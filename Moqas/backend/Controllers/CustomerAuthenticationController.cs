using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model;
using Moqas.Model.Data;
using Moqas.Service;

namespace Moqas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAuthenticationController : ControllerBase
    {
        CustomerContext _context;
        public CustomerAuthenticationController(CustomerContext context) { 
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerRegister request)
        {
            return await CustomerRegisterService.RegisterRequestProcess(this, _context, request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLogin request)
        {
            return await CustomerLoginService.LoginRequestProcess(this, _context, request);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            return await CustomerVerifyService.VerifyRequestProcess(this, _context, token);
        }

    }
}
