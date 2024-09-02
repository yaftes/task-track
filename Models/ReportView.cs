using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
public class ReportView {
    [Required]
    public string? Title {get;set;}
    [Required]
    public string? Detail {get;set;}


}