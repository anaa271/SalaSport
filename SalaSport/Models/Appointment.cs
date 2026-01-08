using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        [Required]
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [StringLength(300)]
        public string? Notes { get; set; }
    }
}
