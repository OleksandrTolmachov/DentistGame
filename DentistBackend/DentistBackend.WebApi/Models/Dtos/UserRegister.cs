using DentistBackend.WebApi.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace DentistBackend.WebApi.Models;

public class UserRegister
{
    [Required]
    [UniqueUsername(ErrorMessage = "Username is taken.")]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
