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
            if (CustomerRegisterService.CheckEmail(_context, request))
            {
                return BadRequest("Customer Already Exists!");
            }
            CustomerRegisterService.CreateCustomer(_context, request.Email, request.Password);
            return Ok("Customer Successfully Created!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLogin request)
        {
            /*var customer = await _context.Customers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (customer == null)
            {
                return BadRequest("Customer Not Found!");
            }

            if (!CustomerLoginService.VerifyPasswordHash(request.Password, customer.PasswordHash, customer.PasswordSalt))
            {
                return BadRequest("Password Is Incorrect!");
            }

            if (customer.VerifiedAt == null)
            {
                return BadRequest("Customer Not Verified!");
            }

            return Ok($"Welcome Back, {customer.Email}");*/
            return await CustomerLoginService.LoginRequestProcess(this, _context, request);

        }

    }
}
