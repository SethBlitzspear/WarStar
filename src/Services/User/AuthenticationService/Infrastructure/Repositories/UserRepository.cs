using Application.Repositories;
using Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;

namespace Infrastructure.Repositories;

public class UserRepository(HttpClient httpClient) : IUserRepository
{

    public async Task<User> GetUserByUsername(string username)
    {
        var response = await httpClient.GetAsync($"api/user/{username}");
        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<User>() ?? throw new KeyNotFoundException($"User with username {username} not found");
        return user;
    }

    public async Task<Guid> CreateUser(User user)
    {
        var response = await httpClient.PostAsJsonAsync("api/user", user);
        response.EnsureSuccessStatusCode();

        // Assuming the API returns the Guid as a plain string (e.g., "f47ac10b-58cc-4372-a567-0e02b2c3d479")
        var responseContent = await response.Content.ReadAsStringAsync();

        // Parse the response content as a Guid and return it
        return Guid.Parse(responseContent.Trim('"'));
    }
}