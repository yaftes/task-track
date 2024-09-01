using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class SubTaskModel {
    public int Id { get; set;}
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    public DateTime Start_Date { get; set;}   
    public DateTime End_Date {get; set;} 

    [Range(1,1000)]
    public int Point {get;set;}
   
  
}