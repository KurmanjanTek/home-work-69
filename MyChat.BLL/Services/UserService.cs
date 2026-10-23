using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    public UserService(IUserRepository userRepository, UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> BlockUserAsync(User user)
    {
        await _userManager.SetLockoutEnabledAsync(user, true);
        var lockoutEnd = new DateTimeOffset(DateTime.UtcNow.AddYears(100));
        var result = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        if (!result.Succeeded)
        {
           return false;
        }
        await _userManager.UpdateSecurityStampAsync(user);

        return true;
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }
    
    public async Task<IdentityResult> CreateAsync(User user, string password) 
    { 
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "user");
        }

        return result;
    }
}