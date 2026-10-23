using Microsoft.EntityFrameworkCore;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.DAL.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly MyChatContext _context;
    
    public  MessageRepository(MyChatContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Message>> GetMessagesAsync()
    {
        var messages = await _context.Messages
            .Include(m => m.User)
            .OrderByDescending(m => m.SendAt).Take(30)
            .OrderBy(m => m.SendAt)
            .ToListAsync();
        
        return messages;
    }
    
    public async Task<int> GetMessageCountByUserIdAsync(int userId)
    {
        return await _context.Messages.CountAsync(m => m.UserId == userId);
    }
   
}