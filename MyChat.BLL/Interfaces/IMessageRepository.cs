using MyChat.Web.Models;

namespace MyChat.BLL.Interfaces;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    
    Task<List<Message>> GetMessagesAsync();
    
    Task<int> GetMessageCountByUserIdAsync(int userId);

}