using Microsoft.AspNetCore.Http;
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

        [HttpGet("profile")]
        public async Task<Model.Profile.Profile> Profile(int Id)
        {
            return await ProfileService.GetProfile(_context, Id);
        }

        [HttpPut("profile-update-email")]
        public async Task<IActionResult> ProfileUpdateEmail(int Id, string newEmail)
        {
            return await ProfileService.UpdateEmail(this, _context, Id, newEmail);
        }

        [HttpPost("profile-update-verify")]
        public async Task<IActionResult> ProfileUpdateVerify(string token, string newEmail)
        {
            return await ProfileService.VerifyRequestProcess(this, _context, token, newEmail);
        }

        [HttpPut("profile-update-password")]
        public async Task<IActionResult> ProfileUpdatePassword(int id, string newPassword)
        {
            return await ProfileService.UpdatePassword(this, _context, id, newPassword);
        }
    }
}
