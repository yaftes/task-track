using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Task {
    [Key]
    public int Id { get; set;}
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    public DateTime Created_At {get;set;}
    public DateTime Start_Date { get; set;}   
    public DateTime End_Date {get; set;} 
    public DateTime Update_Date {get; set;}
    // Navigation Properties
    public Project? Project { get; set;} 

    public ApplicationUser? ApplicationUser { get; set;}
    
    // Foreign Keys

    [ForeignKey(nameof(Project))]
    public int ProjectId {get;set;}

    [ForeignKey(nameof(ApplicationUser))]
    public string? Assigned_to {get;set;}


}