using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class MessageModel {

    [Required]
    [StringLength(100)]
    public string? Text {get;set;}

}