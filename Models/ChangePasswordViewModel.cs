using System.ComponentModel.DataAnnotations;

public class ChangePasswordViewModel
{

    [Required]
    public string? OldPassword { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }
}