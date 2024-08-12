
using System.ComponentModel.DataAnnotations;

public class ProjectModel {
    [Key]
    public int Id { get; set; }
    [Required]    
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = ""; 
    public DateTime CreatedDate { get; set;}    
    public List<Task> Tasks { get; set; } = [];
}
