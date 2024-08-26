using System.ComponentModel.DataAnnotations.Schema;

public class UserSkill {
    public int Id { get; set; }

    [ForeignKey("ApplicationUser")]
    public string? UserId { get; set; }

    [ForeignKey("Skill")]
    public int SkillId { get; set; }   
    // Navigation Properties
    public ApplicationUser? ApplicationUser {get;set;}
    public Skill? Skill {get;set;} 
}