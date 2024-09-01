
using System.ComponentModel.DataAnnotations;
public class RegisterViewModel
{
    [Required]
    public string FirstName { get; set; } = "";
    [Required]
    public string UserName { get; set; } = "";
    [Required]
    public string LastName { get; set; } = "";
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = "";
    
    public string OldPassword {get;set;} = "";

    public List<Skill>? ListofSkill {get;set;}
    public List<int> SelectedSkills { get; set; } = new List<int>();
    
    public List<ApplicationUser>? ApplicationUsers {get;set;}
  
    
}

