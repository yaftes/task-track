

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser {
    [Required]
    public required string FirstName { get; set;}
    [Required]
    public required string LastName { get; set;}
    public DateTime CreatedTime { get; set;}


    // Navigation Properties
    public List<Project>? projects   { get; set;}
    public Task? Task {get;set;}


    // Foreign Keys

}