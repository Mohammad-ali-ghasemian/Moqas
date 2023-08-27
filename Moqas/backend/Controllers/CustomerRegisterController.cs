using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model;
using Moqas.Model.Data;
using Moqas.Service;

namespace Moqas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRegisterController : ControllerBase
    {
        CustomerContext _context;
        public CustomerRegisterController(CustomerContext context) { 
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

        /*[HttpGet("alaki")]
        public async Task<IActionResult> Alaki(CustomerRegister request)*/
    }
}
