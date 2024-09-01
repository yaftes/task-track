using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class TaskWeight {
    [Key]
    public int Id { get; set; }
    [Range(1,1000)]
    public double Point {get;set;} = 0.0;
    public double Weight {get; set;} = 0.0;
    // Navigation Properties
    public virtual Task? Task {get;set;}
    // Foreign Keys
    [ForeignKey("Task")]
    public int TaskId { get; set; } 

}