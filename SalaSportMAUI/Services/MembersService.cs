using System.Net.Http.Json;
using SalaSportMAUI.Models;

namespace SalaSportMAUI.Services;

public class MembersService
{
    private readonly HttpClient _httpClient;

    public MembersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MemberModel?> GetByFullNameAsync(string fullName)
    {
        return await _httpClient.GetFromJsonAsync<MemberModel>(
            $"api/members/by-fullname?fullName={Uri.EscapeDataString(fullName)}");
    }

}
