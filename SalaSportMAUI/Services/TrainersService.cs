using System.Net.Http.Json;
using SalaSportMAUI.Models;

namespace SalaSportMAUI.Services;

public class TrainersService
{
    private readonly HttpClient _httpClient;

    public TrainersService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7267/")
        };
    }

    public async Task<List<TrainerModel>> GetTrainersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TrainerModel>>("api/trainers")
               ?? new List<TrainerModel>();
    }
}
