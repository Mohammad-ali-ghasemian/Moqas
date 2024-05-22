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
        MoqasContext _context;
        public CustomerAuthenticationController(MoqasContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerRegister request)
        {
            return await CustomerRegisterService.RegisterRequestProcess(this, _context, request);
        }

        [HttpPost("send-verification-token")]
        public async Task<IActionResult> SendVerificationToken(string email)
        {
            return await EmailService.SendEmail(this, _context, email, 0);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            return await CustomerVerifyService.VerifyRequestProcess(this, _context, token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLogin request)
        {
            return await CustomerLoginService.LoginRequestProcess(this, _context, request);
        }

        [HttpPost("send-forgotPassword-token")]
        public async Task<IActionResult> SendForgotPasswordToken(string email)
        {
            return await EmailService.SendEmail(this, _context, email, 1);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            return await ResetPasswordService.ResetPasswordProcess(this, _context, request);
        }

        [HttpGet("get-customer")]
        public async Task<IActionResult> GetCustomer(bool token0Email1, string tokenEmail)
        {
            return await CustomerInfoService.GetCustomer(this, _context, token0Email1, tokenEmail);
        }

        [HttpPut("logout")]
        public async Task<IActionResult> Logout(string token)
        {
            return await CustomerLogoutService.LogoutRequestProcess(this, _context, token);
        }

    }
}
