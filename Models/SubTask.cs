using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SubTask {
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
    public Task? Task { get; set;} 
    public SubTaskWeight? SubTaskWeight {get;set;}
    public SubTaskStatus? SubTaskStatus {get;set;}
    public List<SubTaskFile>? SubTaskFiles {get; set;}
    //
    // Foreign Keys
    [ForeignKey("Task")]
    public int TaskId {get;set;}
    public string? Created_by {get;set;}


}