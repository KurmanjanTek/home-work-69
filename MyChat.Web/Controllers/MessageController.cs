using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.Web.Controllers;

public class MessageController : Controller
{
    private readonly MyChatContext _context;
    private readonly IMessageService _service;
    private readonly UserManager<User> _userManager;

    public MessageController(MyChatContext context, IMessageService service, UserManager<User> userManager)
    {
        _context = context;
        _service = service;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Send(string text)
    {
        var user = await _userManager.GetUserAsync(User);
       
        await _service.SendAsync(text, user.Id);

        return Json(new { success = true });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await _context.Messages.Include(m => m.User)
            .OrderByDescending(m => m.SendAt).Take(30).OrderBy(m => m.SendAt)
            .Select(m => new
            {
                id = m.Id,
                text = m.Text,
                sentAt = m.SendAt.ToString("dd.MM.yyyy HH:mm"),
                userName = m.User.UserName,
                avatar = m.User.Avatar,
                userId = m.UserId

            })
            .ToListAsync();
        return Json(messages);
    }
    
   
    


}