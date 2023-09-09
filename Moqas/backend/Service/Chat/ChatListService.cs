using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;

namespace Moqas.Service.Chat
{
    public class ChatListService
    {
        public async static Task<IActionResult> GetChats(ControllerBase controller, MoqasContext context, int customerId, byte filter)
        {
            if (!IsCustomerExist(context, customerId))
            {
                return controller.BadRequest("There is no such customer!");
            }

            List<Model.Data.Chat> chats;
            switch (filter)
            {
                case 0:
                    chats = context.Chats.Where(u => u.CustomerId == customerId).ToList();
                    break;
                case 1:
                    chats = context.Chats.Where(u => u.CustomerId == customerId && u.Status == true).ToList();
                    break;
                case 2:
                    chats = context.Chats.Where(u => u.CustomerId == customerId && u.Status == false).ToList();
                    break;
                default:
                    return controller.BadRequest("Bad filtering!");
            }

            if (chats.Count == 0)
            {
                return controller.BadRequest("No chat found!");
            }

            List<Model.Chat.Chat> convertedChats = new List<Model.Chat.Chat>();
            foreach(var chat in chats)
            {
                convertedChats.Add(new Model.Chat.Chat
                {
                    Id = chat.Id,
                    UserName = chat.UserName,
                    Status = chat.Status,
                    CreatedAt = chat.CreatedAt,
                    CustomerId = chat.CustomerId
                });
            }

            return controller.Ok(convertedChats);
        }

        private static bool IsCustomerExist(MoqasContext context, int customerId)
        {
            if (context.Customers.FirstOrDefault(u => u.Id == customerId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
