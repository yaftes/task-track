
using System.ComponentModel.DataAnnotations;

public class Skill {
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string SkillName { get; set; } = "";

}