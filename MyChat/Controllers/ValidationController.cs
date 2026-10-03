using Microsoft.AspNetCore.Mvc;
using MyChat.Models;

namespace MyChat.Controllers;

public class ValidationController: Controller
{
    private MyChatContext _context;
    
    public ValidationController(MyChatContext context)
    {
        _context = context;
    }
    [AcceptVerbs("GET", "POST")]
    
    public bool CheckUserName(string userName)
    {
        return !_context.Users.Any(u => u.UserName == userName);
    }
    
    public bool CheckEmail(string email)
    {
        return !_context.Users.Any(e => e.Email == email);
    }

    public bool CheckAge(DateOnly birthOfDate)
    {
       return birthOfDate <= DateOnly.FromDateTime(DateTime.Today).AddYears(-18);
    }
}