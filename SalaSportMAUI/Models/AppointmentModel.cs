namespace SalaSportMAUI.Models;

public class AppointmentModel
{
    public int AppointmentId { get; set; }
    public DateTime Date { get; set; }
    public string TrainerName { get; set; } = "";
    public string Status { get; set; } = "";
}
