using MyChat.Web.Models;

namespace MyChat.BLL.Interfaces;

public interface IUserRepository
{
    Task UpdateAsync (User user);
    
    Task<User?> GetByIdAsync(int userId);
}