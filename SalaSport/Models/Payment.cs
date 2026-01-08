using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        [Range(1, 100000)]
        public decimal Amount { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.Now;

        public PaymentType ForType { get; set; }

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public int? MemberSubscriptionId { get; set; }
        public MemberSubscription? MemberSubscription { get; set; }
    }
}
