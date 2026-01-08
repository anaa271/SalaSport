using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 24)]
        public int Months { get; set; }

        [Range(1, 100000)]
        public decimal Price { get; set; }
    }
}
