using Microsoft.AspNetCore.Identity;
using MyChat.Web.Models;

namespace MyChat.Web.Services;

public class AdminInitializer
{
    public static async Task SeedAdminAndRoleData(RoleManager<IdentityRole<int>> _roleManager, UserManager<User> _userManager)
    {
        string adminEmail = "admin@admin.admin";
        string adminPassword = "1qaz@WSX";
        var roles = new[] { "admin", "user" };
        foreach (var role in roles)
        {
            if (await _roleManager.FindByNameAsync(role) is null)
                await _roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        if (await _userManager.FindByEmailAsync(adminEmail) == null)
        {
            User admin = new User() { Email = adminEmail, UserName = adminEmail, Avatar="/images/avatars/default.png" };
            IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(admin, "admin");
        }
    }
}