using System.ComponentModel.DataAnnotations.Schema;

public class SubTaskFile {
    public int Id {get;set;}
    public string? FileName {get;set;}
    public string? ContentType {get;set;}
    public byte[]? Data {get;set;}
    // Navigation Propery
    public SubTask? SubTask {get;set;}  

    // Foreign Keys
    [ForeignKey("SubTask")]
    public int SubTaskId {get;set;}

}