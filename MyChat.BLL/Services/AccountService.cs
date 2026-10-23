using Microsoft.AspNetCore.Identity;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.BLL.Services;

public class AccountService :IAccountService
{
    private readonly UserManager<User> _userManager;

    public AccountService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }
    
    
    public async Task<User?> FindUserAsync(string loginOrEmail) 
    { 
        var user = await _userManager.FindByEmailAsync(loginOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginOrEmail);
        }

        return user;
    }
}