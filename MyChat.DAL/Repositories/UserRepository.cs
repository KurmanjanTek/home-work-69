using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyChatContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(MyChatContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task UpdateAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u=>u.Id == userId);
    }
}