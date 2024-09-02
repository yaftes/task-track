

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ReportFile {
    [Key]
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
    public byte[]? Data { get; set; }

    // Navigation Properties
    public Report? Report { get; set; }
    // Foreign Key
    [ForeignKey("Report")]
    public int ReportId {get;set;}
}
