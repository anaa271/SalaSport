using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models
{
    public class MemberSubscription
    {
        public int MemberSubscriptionId { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        [Required]
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
