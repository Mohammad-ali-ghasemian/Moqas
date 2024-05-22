using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Moqas.Service.Chat;

namespace Moqas.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        MoqasContext _context;
        public ChatController(MoqasContext context)
        {
            _context = context;
        }

        [HttpGet("get-all-messages")]
        public async Task<List<MessagesHistory>> GetAllMessages(int chatId)
        {
            return await ChatService.GetChatMessages(_context, chatId);
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser(int chatId)
        {
            return await ChatService.GetUsername(this, _context, chatId);
        }

        [HttpPost("send-user-message")]
        public async Task<IActionResult> SendUserMessage(int chatId, string message)
        {
            return await ChatService.AddUserMessage(this, _context, chatId, message);
        }

        [HttpPost("send-customer-message")]
        public async Task<IActionResult> SendCustomerMessage(int chatId, string message)
        {
            return await ChatService.AddCustomerMessage(this, _context, chatId, message);
        }

        [HttpGet("get-last-message")]
        public async Task<IActionResult>  GetLastMessage(int chatId)
        {
            return await ChatService.ChatLastMessage(this, _context, chatId);
        }

        [HttpPost("start-chat")]
        public async Task<IActionResult> StartChat(int customerId, string username)
        {
            return await ChatService.StartChat(this, _context, customerId, username);
        }

        [HttpPost("email-new-chat")]
        public async Task<IActionResult> EmailNewChat(int customerId)
        {
            return await ChatService.EmailNewChat(this, _context, customerId);
        }

        [HttpPut("end-chat")]
        public async Task<IActionResult> EndChat(int chatId)
        {
            return await ChatService.EndChat(this, _context, chatId);
        }

    }
}
