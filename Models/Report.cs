
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Report {
    [Key]
    public int Id {get;set;}
    public string? Title {get;set;}
    public string? Detail {get;set;}
    public int TaskId {get;set;}
    // Navigation Properties
    public Project? Project {get;set;}    
    public List<ReportFile>? ReportFiles {get;set;}
    
    // Foreign Keys
    [ForeignKey("Project")]
    public int ProjectId {get;set;}

   


}