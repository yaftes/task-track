
using System.ComponentModel.DataAnnotations;

public class Skill {
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string SkillName { get; set; }
    public List<ApplicationUser> ApplicationUsers { get; set; } = [];
}