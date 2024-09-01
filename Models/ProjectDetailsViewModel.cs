public class ProjectDetail {
    public Project? Project {get;set;}
    public List<ApplicationUser>? Projectmembers {get;set;}
    public List<ApplicationUser>? AvailableUsers {get;set;}
    public List<Project>? Projects {get;set;}
    public List<Task>? Tasks {get;set;} 
    public Task? Task {get;set;}
    public TaskModel? TaskModel {get;set;}
    public List<Message>? Messages {get;set;}
    public InvitationModel? InvitationModel {get;set;}
    public List<Invitation>? Invitations {get;set;}
    public List<TaskWeight>? TaskWeight {get;set;}
    public List<ProjectFile>? ProjectFiles {get;set;}

}