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
    public double Progress {get;set;} = 0.0;
    
    // Navigation Properties
    public Project? Project { get; set;} 
    public TaskWeight? TaskWeight {get;set;}
    public List<SubTask>? SubTasks {get;set;}
    public List<Invitation>? Invitations {get;set;}
    public TaskStatus? TaskStatus {get;set;}
    public virtual List<TaskFile>? TaskFiles {get;set;}

    //
    // Foreign Keys
    [ForeignKey("Project")]
    public int ProjectId {get;set;}
    // 
    //
    public string? Assigned_to {get;set;}
    public string? Name {get;set;}
    public string? Created_by {get;set;}

}


