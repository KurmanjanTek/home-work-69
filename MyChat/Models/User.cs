using Microsoft.AspNetCore.Identity;

namespace MyChat.Models;

public class User : IdentityUser<int>
{
    public DateOnly BirthOfDate {get; set;}
    public string Avatar { get; set; }
    
}