using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models;

public class Trainer
{
    public int TrainerId { get; set; }

    [Required(ErrorMessage = "Numele este obligatoriu.")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [RegularExpression(@"^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
        ErrorMessage = "Telefon invalid.")]
    public string? Phone { get; set; }

    public string? UserId { get; set; } 
}
