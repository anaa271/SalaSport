using System.Net.Http.Json;
using SalaSportMAUI.Models;

namespace SalaSportMAUI.Services;

public class SubscriptionsService
{
    private readonly HttpClient _httpClient;

    public SubscriptionsService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7267/")
        };
    }

    public async Task<List<SubscriptionModel>> GetSubscriptionsForMemberAsync(int memberId)
    {
        try
        {
            return await _httpClient
                .GetFromJsonAsync<List<SubscriptionModel>>(
                    $"api/subscriptions/member/{memberId}")
                ?? new List<SubscriptionModel>();
        }
        catch
        {
            return new List<SubscriptionModel>();
        }
    }


}
