

using System.ComponentModel.DataAnnotations.Schema;

public class ProjectMember {
    public int Id { get; set; }

    [ForeignKey(nameof(ApplicationUser))]
    public string? UserId {get;set;}

    [ForeignKey(nameof(Project))]
    public int ProjId {get;set;}
    public DateTime Joined_At {get;set;}
    // Navigation Properties
    public ApplicationUser? ApplicationUser {get;set;}
    public Project? Project {get;set;}

}