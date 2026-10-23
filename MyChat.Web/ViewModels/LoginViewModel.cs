using System.ComponentModel.DataAnnotations;

namespace MyChat.Web.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Введите свой логин или Email")]
    public string LoginOrEmail { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
    
    public string? ReturnUrl { get; set; }
}