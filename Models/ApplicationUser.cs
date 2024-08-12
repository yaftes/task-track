

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser {
    [Required]
    public required string FirstName { get; set;}
    [Required]
    public required string LastName { get; set;}
    public DateTime CreatedTime { get; set;}
    public List<Skill>? Skills {get;set;}
    public List<Project>? Projects {get;set;}
    public List<Task>? Tasks {get;set;} 
    
}