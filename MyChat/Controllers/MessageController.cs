using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyChat.Models;

namespace MyChat.Controllers;

public class MessageController : Controller
{
    public MyChatContext _context;
    private readonly UserManager<User> _userManager;

    public MessageController(MyChatContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Send(string text)
    {
        var user = await _userManager.GetUserAsync(User);
        Message message = new Message
        {
            Text = text,
            SendAt = DateTime.Now,
            UserId = user.Id
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

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