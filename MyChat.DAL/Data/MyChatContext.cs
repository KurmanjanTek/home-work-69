using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Web.Models;

public class MyChatContext: IdentityDbContext<User, IdentityRole<int>, int>
{
    private DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public MyChatContext(DbContextOptions<MyChatContext> options) : base(options) {}
}