using Microsoft.AspNetCore.Identity;
using MyChat.Web.Models;

namespace MyChat.BLL.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(User user, string password);
    
    Task<User?> FindUserAsync(string loginOrEmail);
}