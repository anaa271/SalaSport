namespace SalaSportMAUI.Models;

public class SubscriptionModel
{
    public int SubscriptionId { get; set; }
    public string Type { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
