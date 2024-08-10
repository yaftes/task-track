
using System.ComponentModel.DataAnnotations;

public class Skill {
    [Key]
    public int Id { get; set; }
    public required string SkillName { get; set; }
    public required List<ApplicationUser> ApplicationUsers { get; set; }
}