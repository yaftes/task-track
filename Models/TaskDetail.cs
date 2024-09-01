public class TaskDetail {
    public Task? Task { get; set; }
    public List<SubTask>? SubTasks { get; set; }
    public List<SubTaskWeight>? SubTaskWeights {get;set;}
    public Invitation? Invitation { get; set; }
    public List<TaskFile>? TaskFiles { get; set; }
}