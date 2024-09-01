
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProjectStatus {
    [Key]
    public int Id {get;set;}
    public string? Status {get;set;}
    // Navigation Properties
    public Project? Project {get;set;}
    // foreign Key
    [ForeignKey("Project")]
    public int ProjectId {get;set;}
}