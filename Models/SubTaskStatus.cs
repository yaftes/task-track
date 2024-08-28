
public class SubTaskStatus {
    public int Id {get;set;}
    public string Status {get;set;}

    // Navigation Properties
    public SubTask? SubTask {get;set;}

    // foreign Key
    [ForeignKey("SubTask")]
    public int SubTaskId {get;set;}
}