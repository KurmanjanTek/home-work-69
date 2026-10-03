namespace MyChat.ViewModels;

public class EditProfileViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthOfDate { get; set; }
    public IFormFile? Avatar { get; set; }
}