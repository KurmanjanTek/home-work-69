using MyChat.Web.Models;

namespace MyChat.BLL.Interfaces;

public interface IMessageService
{
    Task SendAsync(string text, int userId);

    Task<List<Message>> GetMessagesAsync();
    
    Task<int> GetMessageCountByUserIdAsync(int userId);
}