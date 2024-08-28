
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SubTaskStatus {
    [Key]
    public int Id {get;set;}
    public string? Status {get;set;}
    // Navigation Properties
    public SubTask? SubTask {get;set;}
    // foreign Key
    [ForeignKey("SubTask")]
    public int SubTaskId {get;set;}
}