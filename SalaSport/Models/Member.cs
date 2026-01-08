using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models;

public class Member
{
    public int MemberId { get; set; }

    [Required(ErrorMessage = "Numele este obligatoriu.")]
    [StringLength(100, ErrorMessage = "Numele poate avea maxim 100 caractere.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Emailul este obligatoriu.")]
    [EmailAddress(ErrorMessage = "Email invalid.")]
    public string Email { get; set; } = string.Empty;

    [RegularExpression(@"^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
        ErrorMessage = "Telefon invalid. Exemplu: 0722-123-123 / 0722 123 123")]
    public string? Phone { get; set; }

    public string? UserId { get; set; } 
}
