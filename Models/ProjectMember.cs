

using System.ComponentModel.DataAnnotations;

public class ProjectMember {
    [Key]
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int ProjectId { get; set;}
    public DateTime Joined_At { get; set; }
    public Project? Project {get;set;}

}
