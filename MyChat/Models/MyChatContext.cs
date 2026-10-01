using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Models;

public class MyChatContext: IdentityDbContext<User, IdentityRole<int>, int>
{
    private DbSet<User> Users { get; set; }
    public MyChatContext(DbContextOptions<MyChatContext> options) : base(options) {}
}