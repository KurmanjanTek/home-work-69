using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyChat.BLL.Interfaces;
using MyChat.Web.Models;

namespace MyChat.Web.Controllers;

public class MessageController : Controller
{
    private readonly IMessageService _service;
    private readonly UserManager<User> _userManager;

    public MessageController(IMessageService service, UserManager<User> userManager)
    {
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
        var messages = await _service.GetMessagesAsync();
        
        return Json(messages);
    }
    
   
    


}