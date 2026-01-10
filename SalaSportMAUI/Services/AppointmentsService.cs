using System.Net.Http.Json;
using SalaSportMAUI.Models;

namespace SalaSportMAUI.Services;

public class AppointmentsService
{
    private readonly HttpClient _httpClient;

    public AppointmentsService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7267/")


        };
    }

    public async Task<List<AppointmentModel>> GetAppointmentsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AppointmentModel>>("api/appointments")
               ?? new List<AppointmentModel>();
    }
}
