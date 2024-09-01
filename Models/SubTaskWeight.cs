using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class SubTaskWeight  {
    [Key]
    public int Id { get; set; }
    [Range(1,1000)]
    public double Point {get;set;} = 0.0;
    public double Weight {get; set;} = 0.0;
    // Navigation Properties
    public SubTask? SubTask {get;set;}
    // Foreign Keys
    [ForeignKey("SubTask")]
    public int SubTaskId { get; set;} 

}
