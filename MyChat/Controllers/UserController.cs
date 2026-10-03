using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MyChat.Models;
using MyChat.ViewModels;

namespace MyChat.Controllers;
[Authorize]
public class UserController: Controller
{
    public readonly MyChatContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserController(MyChatContext context, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _userManager=userManager;
        _webHostEnvironment=webHostEnvironment;
    }
    
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        return View(currentUser);
    }
    [Authorize(Roles="admin, user")]
    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var model = new EditProfileViewModel
        {
            UserName = currentUser.UserName,
            Email = currentUser.Email,
            BirthOfDate = currentUser.BirthOfDate
        };
        return View(model);
    }
    [Authorize(Roles="admin, user")]
    [HttpPost]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        currentUser.UserName = model.UserName;
        currentUser.Email=model.Email;
        currentUser.BirthOfDate = model.BirthOfDate;

        if (model.Avatar != null)
        {
            var wwwPath=_webHostEnvironment.WebRootPath;
            var path=Path.Combine(wwwPath, "images", "avatars");
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension=Path.GetExtension(model.Avatar.FileName);
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".jfif" };
              
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join(",", allowedExtensions)} are allowed");
            }

            var newFileName = $"{Guid.NewGuid()}{extension}";
            currentUser.Avatar = $"/images/avatars/{newFileName}";
            var fileWithPath=Path.Combine(path, newFileName);
            using var fileStream=new FileStream(fileWithPath, FileMode.Create);
            await model.Avatar.CopyToAsync(fileStream);
        }
        await _userManager.UpdateAsync(currentUser);
        return RedirectToAction("Index");
    }
    [Authorize(Roles="admin")]
    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }
    
    [Authorize(Roles="admin")]
    [HttpGet]
    public async Task<ActionResult> BlockUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
      await _userManager.SetLockoutEnabledAsync(user, true);

        var lockoutEnd = new DateTimeOffset(DateTime.UtcNow.AddYears(100));
        var result = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("Users");
        }
        ModelState.AddModelError("", "Ошибка при блокировке пользователя.");
        return View();
    }
    [Authorize(Roles="admin")]
    [HttpGet]
    public async Task<IActionResult> EditProfile(int id)
    {
        var currentUser = await _userManager.FindByIdAsync(id.ToString());
        if (_userManager == null)
        {
            return NotFound();
        }

        var model = new EditProfileViewModel
        {
            UserName = currentUser.UserName,
            Email = currentUser.Email,
            BirthOfDate = currentUser.BirthOfDate
        };
        return View(model);
    }
    [Authorize(Roles="admin")]
    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model, int id )
    {
        var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id==id);
        if (user == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        user.UserName=model.UserName;
        user.Email=model.Email;
        user.BirthOfDate=model.BirthOfDate;
        
        if (model.Avatar != null)
        {
            var wwwPath=_webHostEnvironment.WebRootPath;
            var path=Path.Combine(wwwPath, "images", "avatars");
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension=Path.GetExtension(model.Avatar.FileName);
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".jfif" };
              
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join(",", allowedExtensions)} are allowed");
            }

            var newFileName = $"{Guid.NewGuid()}{extension}";
            user.Avatar = $"/images/avatars/{newFileName}";
            var fileWithPath=Path.Combine(path, newFileName);
            using var fileStream=new FileStream(fileWithPath, FileMode.Create);
            await model.Avatar.CopyToAsync(fileStream);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Users", "User", new{id=user.Id});
    }
    [Authorize(Roles="admin")]
    [HttpGet]
    public IActionResult CreateProfile()
    {
        return View();
    }
    [Authorize(Roles="admin")]
    [HttpPost]
    public async Task<IActionResult> Create(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                BirthOfDate = model.BirthOfDate,
                Avatar = "/images/avatars/default.png"
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Profile(int id)
    { 
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }
        var messageCount = await _context.Messages.CountAsync(m => m.UserId == user.Id);
        ViewBag.MessageCount = messageCount;
        return View(user);
    }
}