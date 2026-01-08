using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models;

public class Appointment
{
    public int AppointmentId { get; set; }

    [Required(ErrorMessage = "Selectează un membru.")]
    public int MemberId { get; set; }
    public Member? Member { get; set; }

    [Required(ErrorMessage = "Selectează un trainer.")]
    public int TrainerId { get; set; }
    public Trainer? Trainer { get; set; }

    [Required(ErrorMessage = "Ora de început este obligatorie.")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "Ora de final este obligatorie.")]
    public DateTime EndTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    [StringLength(300, ErrorMessage = "Notele pot avea maxim 300 caractere.")]
    public string? Notes { get; set; }
}
