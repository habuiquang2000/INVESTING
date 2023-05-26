using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;

namespace Gateway.Implementations;

public class BaseHttpClient
{
    public string _baseUri;
    public HttpClient _httpClient;
    public BaseHttpClient(string baseUri)
    {
        _baseUri = baseUri;
        //HttpClientHandler handler = new();
        //_httpClient = new(handler)
        _httpClient = new()
        {
            BaseAddress = new Uri(_baseUri)
        };
        _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");

    }

    public T? Get<T>(
        string path,
        Dictionary<string, string?> query
    )
    {
        HttpRequestMessage httpRequest = new(
            HttpMethod.Get,
            QueryHelpers.AddQueryString(
                path,
                query
            ));

        HttpResponseMessage objResult = _httpClient.SendAsync(httpRequest).Result;
        string response = objResult.Content.ReadAsStringAsync().Result;

        return JsonConvert.DeserializeObject<T>(response);
    }

    public T? Post<T>(
        string path,
        object? body = null
    )
    {
        string strDataJson = JsonConvert.SerializeObject(body);
        HttpRequestMessage httpRequest = new(
            HttpMethod.Post,
            path
        )
        {
            Content = new StringContent(strDataJson, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage objResult = _httpClient.SendAsync(httpRequest).Result;
        string response = objResult.Content.ReadAsStringAsync().Result;

        return JsonConvert.DeserializeObject<T>(response);
    }

    public async Task<object> Delete(
        string path
    ) => await _httpClient.DeleteAsync(path);
}
