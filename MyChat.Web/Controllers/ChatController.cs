using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyChat.Web.Models;

namespace MyChat.Web.Controllers;

public class ChatController:Controller
{
    public MyChatContext _context;
    private readonly UserManager<User> _userManager;

    public ChatController(MyChatContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

}