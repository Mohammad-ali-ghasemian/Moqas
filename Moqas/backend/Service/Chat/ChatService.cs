﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqas.Model.Authentication;
using Moqas.Model.Chat;
using Moqas.Model.Data;
using Moqas.Service.Authentication;
using Org.BouncyCastle.Asn1.Ocsp;

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



        public async static Task<IActionResult> StartChat(ControllerBase controller, MoqasContext context, string configUsername, string configPassword, string username)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(u => u.ConfigUsername == configUsername);

            if (customer == null)
            {
                return controller.BadRequest("Config Not Found!");
            }

            if (!CustomerLoginService.VerifyPasswordHash(configPassword, customer.ConfigPasswordHash, customer.ConfigPasswordSalt))
            {
                return controller.BadRequest("Password Is Incorrect!");
            }


            var newChat = new Model.Data.Chat
            {
                UserName = username,
                CreatedAt = DateTime.Now,
                CustomerId = customer.Id
            };
            context.Chats.Add(newChat);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok(newChat.Id);
        }



        public async static Task<IActionResult> EmailNewChat(ControllerBase controller, MoqasContext context, int customerId)
        {
            var customer = context.Customers.FirstOrDefault(u => u.Id == customerId);
            if (customer == null)
            {
                return controller.BadRequest("There is no such customer with this id!");
            }

            return await EmailService.SendEmail(controller, context, customer.Email, 2);
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
        

        
        public async static Task<IActionResult> SubmitRate(ControllerBase controller, MoqasContext context, int chatId, byte rate)
        {
            if (rate < 1 || rate > 5)
            {
                return controller.BadRequest("Invalid Rate Number!");
            }

            var chat = context.Chats.FirstOrDefault(u => u.Id == chatId);
            if (chat == null)
            {
                return controller.BadRequest("There is no such chat!");
            }

            chat.Rate = rate;
            context.Chats.Update(chat);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok("Rate Submitted!");
        }



        /*public async static Task<IActionResult> GetRates(ControllerBase controller, MoqasContext context, int customerId)
        {
            var chat = context.Chats.FirstOrDefault(u => u.Id == customerId);
            if (chat == null)
            {
                return controller.BadRequest("There is no such chat!");
            }

            context.Chats.Update(chat);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (ObjectDisposedException ex) { }
            return controller.Ok("Chat Ended!");
        }*/
        
    }
}
