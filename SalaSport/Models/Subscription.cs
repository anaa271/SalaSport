using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models;

public class Subscription
{
    public int SubscriptionId { get; set; }

    [Required(ErrorMessage = "Denumirea abonamentului este obligatorie.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 24, ErrorMessage = "Lunile trebuie să fie între 1 și 24.")]
    public int Months { get; set; }

    [Range(1, 100000, ErrorMessage = "Prețul trebuie să fie mai mare ca 0.")]
    public decimal Price { get; set; }
}
