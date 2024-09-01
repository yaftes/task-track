using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Invitation {
    [Key]
    public int Id {get;set;}
    public string? Sender_Id {get;set;}
    public string? Sender_Name {get;set;}   
    public string? Recepant_Id {get;set;}
    public string? Receiver_Name {get;set;}
    public DateTime Created_At {get;set;}
    public string? Title {get;set;}
    public string? Description {get;set;}
    public int ProjectId {get;set;}  
    public string? status {get;set;}
    // Navigation Property
    public Task? Task {get;set;}
    // Foreign Key
    [ForeignKey("Task")]
    public int TaskId {get;set;}
  
}
