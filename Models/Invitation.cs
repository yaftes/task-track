

public class Invitation {
    //Primary Key
    public int Id {get; set;} 

    // foreign keys 
    public int ProjectId {get; set;}    
    public string? Sender_Id {get; set;} 
    public string? Recipent_Id {get; set;}  
    public bool status = false;
    public DateTime Sent_At {get;set;}


}