using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyChat.Web.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Логин")]
    [Remote(action: "CheckUserName", controller:"Validation", ErrorMessage = "Такой логин уже существует")]
    public string UserName { get; set; }
    [Required]
    [Remote(action: "CheckEmail", controller:"Validation", ErrorMessage = "Такой email уже существует")]
    [Display(Name = "Адрес электронной почты")]
    public string Email { get; set; }
    [Remote(action: "CheckAge", controller:"Validation", ErrorMessage = "Возвраст пользователя должен быть не менее 18")]
    public DateOnly BirthOfDate { get; set; }
    [Required]
    [Display(Name = "Аватар")]
    public IFormFile  Avatar { get; set; }
    [Required] 
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{6,}$", ErrorMessage = "Пароль должен содержать в себе 1 букву верхнего регистра, 1 букву нижнего регистра а также минимум одну цифру и длина пароля не менее 6 символов")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
}