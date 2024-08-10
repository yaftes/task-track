using System.ComponentModel.DataAnnotations;

public class CreateRole {
    [Required]
    public required string RoleName { get; set; }    
}