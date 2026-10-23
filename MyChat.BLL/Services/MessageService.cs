using Microsoft.AspNetCore.Identity;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task SendAsync(string text, int userId)
    {
        Message message = new Message
        {
            Text = text,
            SendAt = DateTime.UtcNow,
            UserId = userId
        };

        await _messageRepository.AddAsync(message);
    }

    public async Task<List<Message>> GetMessagesAsync()
    {
        var messages= await _messageRepository.GetMessagesAsync();
       
        return  messages;
    }
    
    public async Task<int> GetMessageCountByUserIdAsync(int userId)
    {
        return await _messageRepository.GetMessageCountByUserIdAsync(userId);
    }
}