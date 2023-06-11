using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameClient 
{
    private string baseUrl;

    public GameClient(ServerOptions serverOptions)
    {
        baseUrl = serverOptions.BaseUrl;
    }

    public async Task InformLevelFinished()
    {
        using HttpClient client = new HttpClient();
        string registerUrl = baseUrl + "player/postfinishedlevel";

        string token = PlayerPrefs.GetString("token");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

        HttpResponseMessage response = await client.PostAsync(registerUrl, null);
    }

    public async Task<IEnumerable<PlayerStats>> GetAllPlayers()
    {
        using HttpClient client = new HttpClient();
        string registerUrl = baseUrl + "player/getallplayers";

        string token = PlayerPrefs.GetString("token");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

        HttpResponseMessage response = await client.GetAsync(registerUrl);

        string responseBody = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<IEnumerable<PlayerStats>>(responseBody);
        return users;
    }

    public async Task Register(string username, string password)
    {
        using HttpClient client = new HttpClient();
        string registerUrl = baseUrl + "account/register";

        var requestData = new
        {
            username = username,
            password = password
        };

        string jsonContent = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(registerUrl, content);

        string responseBody = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(responseBody);
        PlayerPrefs.SetString("token", user.Token);
        PlayerPrefs.SetString("username", user.Username);
    }
}
