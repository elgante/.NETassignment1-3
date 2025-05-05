using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTO;
using Domain.Models;


public class UserHttpClient : IUserService
{

    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User> Create (UserCreateDto dto)
    {
        var response = await client.PostAsJsonAsync("/Users", dto);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var user =
            JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            })!;

        return user; 
        
      /*  HttpResponseMessage response = await client.PostAsJsonAsync("/Users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;*/
    }

    public async Task<IEnumerable<User>> GetUsers (string? usernameContains = null)
    {
        string uri = "/users";
        if (!string.IsNullOrEmpty(usernameContains))
        {
            uri += $"?username={usernameContains}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
    
}
