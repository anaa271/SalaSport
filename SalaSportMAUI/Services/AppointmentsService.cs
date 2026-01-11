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
        return await _httpClient
            .GetFromJsonAsync<List<AppointmentModel>>("api/appointments")
            ?? new List<AppointmentModel>();
    }

    public async Task<List<AppointmentModel>> GetAppointmentsForMemberAsync(int memberId)
    {
        return await _httpClient
            .GetFromJsonAsync<List<AppointmentModel>>($"api/appointments/member/{memberId}")
            ?? new List<AppointmentModel>();
    }

    public async Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment)
    {
        var response = await _httpClient.PostAsJsonAsync("api/appointments", appointment);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AppointmentModel>();
    }



    public async Task UpdateAppointmentAsync(AppointmentModel appointment)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/appointments/{appointment.AppointmentId}", appointment);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAppointmentAsync(int appointmentId)
    {
        var response = await _httpClient.DeleteAsync($"api/appointments/{appointmentId}");
        response.EnsureSuccessStatusCode();
    }
}
