using System.Text.Json.Serialization;

namespace SalaSportMAUI.Models;

public class SubscriptionModel
{
    [JsonPropertyName("memberSubscriptionId")]
    public int MemberSubscriptionId { get; set; }

    [JsonPropertyName("subscriptionId")]
    public int SubscriptionId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("months")]
    public int Months { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}
