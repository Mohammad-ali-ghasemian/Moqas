using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Chat;
using Moqas.Model.Data;

namespace Moqas.Service.Chat
{
    public class ChatService
    {
        public async static Task<List<MessagesHistory>> GetChatMessages(MoqasContext context ,int chatId)
        {
            return context.MessagesHistory.Where(u => u.ChatId == chatId).ToList();
        }

        public async static Task<IActionResult> GetUsername(ControllerBase controller, MoqasContext context, int chatId)
        {
            var chat = context.Chats.FirstOrDefault(u => u.Id == chatId);
            if (chat == null)
            {
                return controller.BadRequest("There is no such chat!");
            }
            return controller.Ok(chat.UserName);
        }



        public async static Task<IActionResult> AddUserMessage(ControllerBase controller, MoqasContext context, int chatId, string message)
        {
            var sender = context.Chats.FirstOrDefault(u => u.Id == chatId).UserName;
            if (sender == null)
            {
                return controller.BadRequest("There is no such chatId!");
            }
            AddMessage(controller, context, chatId, message, sender);
            return controller.Ok("Message Inserted!");
        }

        public async static Task<IActionResult> AddCustomerMessage(ControllerBase controller, MoqasContext context, int chatId, string message)
        {
            var sender = context.Customers.FirstOrDefault(u => u.Id == context.Chats.FirstOrDefault(u => u.Id == chatId).CustomerId).Email;
            if (sender == null)
            {
                return controller.BadRequest("There is no such chatId!");
            }
            AddMessage(controller, context, chatId, message, sender);
            return controller.Ok("Message Inserted!");
        }

        private async static void AddMessage(ControllerBase controller, MoqasContext context, int chatId, string message, string sender)
        {
            var newMessage = new MessagesHistory
            {
                ChatId = chatId,
                Sender = sender,
                Message = message,
                CreatedAt = DateTime.Now
            };
            context.MessagesHistory.Add(newMessage);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
        }

        public async static Task<IActionResult> ChatLastMessage(ControllerBase controller, MoqasContext context, int chatId)
        {
            var chat = context.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                return controller.BadRequest("There is no such chat!");
            }
            /*var message = context.MessagesHistory.LastOrDefault(u => u.ChatId == chatId);*/
            var message = context.MessagesHistory
                .Where(u => u.ChatId == chatId)
                .OrderByDescending(u => u.CreatedAt)
                .FirstOrDefault();
            if (message == null)
            {
                return controller.Ok("No message found!");
            }
            return controller.Ok(new Message
            {
                Id = message.Id,
                Sender = message.Sender,
                Text = message.Message,
                CreatedAt = message.CreatedAt
            });
        }



        public async static Task<IActionResult> StartChat(ControllerBase controller, MoqasContext context, int customerId, string username)
        {
            var newChat = new Model.Data.Chat
            {
                UserName = username,
                CreatedAt = DateTime.Now,
                CustomerId = customerId
            };
            context.Chats.Add(newChat);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok("New Chat Inserted!");
        }

        public async static Task<IActionResult> EndChat(ControllerBase controller, MoqasContext context,int chatId)
        {
            var chat = context.Chats.FirstOrDefault(u => u.Id == chatId);
            if (chat == null)
            {
                return controller.BadRequest("There is no such chat!");
            }
            chat.Status = true;
            context.Chats.Update(chat);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok("Chat Ended!");
        }
    }
}
