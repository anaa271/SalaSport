using System.Text.Json.Serialization;

public class AppointmentModel
{
    [JsonPropertyName("appointmentId")]
    public int AppointmentId { get; set; }

    [JsonPropertyName("memberId")]
    public int MemberId { get; set; }

    [JsonPropertyName("trainerId")]
    public int TrainerId { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}
