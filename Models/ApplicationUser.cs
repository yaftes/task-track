

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser {
    [Required]
    public required string FirstName { get; set;}
    [Required]
    public required string LastName { get; set;}
    public DateTime CreatedTime { get; set;}


    // Navigation Properties
    public virtual List<Project> Projects { get; set;} = new();
    public virtual List<Skill> Skills { get; set;} = new();
    public virtual List<Task> Tasks {get;set;} = new();

    // Foreign Keys

}