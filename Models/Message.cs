using System.ComponentModel.DataAnnotations.Schema;
public class Message {
    public int Id {get;set;}
    public string Text {get;set;} = "";
    public string CreatorId {get;set;} = "";
    public string FullName {get;set;} = "";
    public DateTime Created_At {get;set;}
    // Navigation Properties
    public Project? Project {get;set;}
    // Foreign Keys
    [ForeignKey("Project")]
    public int ProjectId {get;set;} 

}