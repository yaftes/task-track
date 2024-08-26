using System.ComponentModel.DataAnnotations;
public class ProjectModel {
    public int ProjectId { get; set; }
    [Required]   
    [StringLength(100)] 
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    public string Start_Date {get; set;} = "";
    public string End_Date {get; set;} = "";

    public Project project {get;set;}
    public List<ApplicationUser> ProjectMembers  {get;set;} = new ();    


}

