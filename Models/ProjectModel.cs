using System.ComponentModel.DataAnnotations;
public class Project {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = ""; 
    public DateTime Created_At {get;set;}
    public DateTime Start_Date { get; set;}   
    public DateTime End_Date {get; set;} 
    public DateTime Update_Date {get; set;}
  


}
