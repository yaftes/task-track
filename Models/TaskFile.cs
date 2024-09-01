using System.ComponentModel.DataAnnotations.Schema;

public class TaskFile  {
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
    public byte[]? Data { get; set; }
    // Navigation Properties
    public Task? Task {get;set;} 
    // Foreign Key
    [ForeignKey("Task")]
    public int TaskId { get; set; }  
}