using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Moqas.Service.Authentication;
using Moqas.Service.Profile;

namespace Moqas.Controllers.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        MoqasContext _context;
        public ProfileController(MoqasContext context)
        {
            _context = context;
        }

        [HttpGet("get-customer-profile")]
        public async Task<Model.Profile.Profile> Profile(int customerId)
        {
            return await ProfileService.GetProfile(_context, customerId);
        }

        //this method creates new verification token based on the customer ID
        [HttpPut("profile-update-email")]
        public async Task<IActionResult> ProfileUpdateEmail(int customerId, string newEmail)
        {
            return await ProfileService.UpdateEmail(this, _context, customerId, newEmail);
        }

        //this method sends new verification token for the old email, to the new email
        [HttpPost("email-verification-code")]
        public async Task<IActionResult> EmailVerificationCode(int customerId, string newEmail)
        {
            return await EmailService.SendNewToken(this, _context, customerId, newEmail);
        }

        //this method verify the verification code and update the email with the new one
        [HttpPost("profile-update-email-verify")]
        public async Task<IActionResult> ProfileUpdateVerify(string verificationToken, string newEmail)
        {
            return await ProfileService.VerifyRequestProcess(this, _context, verificationToken, newEmail);
        }

        [HttpPut("profile-update-password")]
        public async Task<IActionResult> ProfileUpdatePassword(int customerId, string newPassword)
        {
            return await ProfileService.UpdatePassword(this, _context, customerId, newPassword);
        }
    }
}
