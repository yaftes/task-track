using System.ComponentModel.DataAnnotations.Schema;

public class ProjectFile {
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
    public byte[]? Data { get; set; }
    // Navigation Properties
    public Project? Project {get;set;} 
    // Foreign Key
    [ForeignKey("Project")]
    public int ProjectId { get; set; }  
}