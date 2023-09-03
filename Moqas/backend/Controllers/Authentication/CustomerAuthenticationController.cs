using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Authentication;
using Moqas.Model.Data;
using Moqas.Service.Authentication;

namespace Moqas.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAuthenticationController : ControllerBase
    {
        CustomerContext _context;
        public CustomerAuthenticationController(CustomerContext context)
        {
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return await ForgotPasswordService.ForgotPasswordEmail(this, _context, email);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            return await ResetPasswordService.ResetPasswordProcess(this, _context, request);
        }

    }
}
