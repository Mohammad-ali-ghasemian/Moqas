using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Moqas.Service.Chat;

namespace Moqas.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatListController : ControllerBase
    {
        MoqasContext _context;
        public ChatListController(MoqasContext context)
        {
            _context = context;
        }

        [HttpGet("get-all-chats")]
        public async Task<IActionResult> GetAllChats(int customerId)
        {
            return await ChatListService.GetChats(this, _context, customerId, 0);
        }

        [HttpGet("get-finished-chats")]
        public async Task<IActionResult> GetFinishedChats(int customerId)
        {
            return await ChatListService.GetChats(this, _context, customerId, 1);
        }

        [HttpGet("get-unfinished-chats")]
        public async Task<IActionResult> GetUnfinishedChats(int customerId)
        {
            return await ChatListService.GetChats(this, _context, customerId, 2);
        }

    }
}
