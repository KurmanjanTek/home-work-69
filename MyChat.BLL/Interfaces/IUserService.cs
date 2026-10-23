using Microsoft.AspNetCore.Identity;
using MyChat.Web.Models;


namespace MyChat.BLL.Interfaces;

public interface IUserService
{
    Task UpdateAsync (User user);
    
    Task<bool> BlockUserAsync(User user);
    
    Task<User?> GetByIdAsync(int userId);
    
    Task<IdentityResult> CreateAsync(User user, string password);
}