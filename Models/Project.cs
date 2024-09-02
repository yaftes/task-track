using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Project {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    public DateTime Created_At {get;set;}
    public DateTime Start_Date { get; set;}   
    public DateTime End_Date {get; set;} 
    public DateTime Update_Date {get; set;}
    public double Progress {get;set;} = 0.0;
    // Navigation Properties
    public ApplicationUser? ApplicationUser { get; set; }
    public  List<Task>? Tasks { get; set;} 
    public List<Message>? Messages {get;set;}
    public ProjectStatus? ProjectStatus { get; set; } 
    public List<ProjectFile>? ProjectFiles { get; set; }
    public List<Report>? Reports { get; set; }
    // Foreign Keys
    [ForeignKey("ApplicationUser")]
    public string? Created_By { get; set;}

}
