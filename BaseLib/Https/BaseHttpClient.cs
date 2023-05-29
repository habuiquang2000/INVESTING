using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;

namespace BaseLib.Https;

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
        _httpClient.DefaultRequestHeaders.Clear();
        //_httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("vi-VN"));

        _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
        //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //var response = await client.GetAsync(string.format("api/products/id={0}&type={1}", param.Id.Value, param.Id.Type)).Result;
        //if (response.IsSuccessStatusCode)
        //{
        //    var result = response.Content.ReadAsStringAsync().Result;
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "fail");
    }

    public async Task<T?> GetAsync<T>(
        string path,
        Dictionary<string, string?> query
    )
    {
        HttpRequestMessage req = new(
            HttpMethod.Get,
            QueryHelpers.AddQueryString(
                path,
                query
            ));

        //HttpResponseMessage res = _httpClient.SendAsync(req).Result;
        HttpResponseMessage res = await _httpClient.SendAsync(req);
        //string result = res.Content.ReadAsStringAsync().Result;
        string result = await res.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task<T?> PostAsync<T>(
        string path,
        object? body = null
    )
    {
        string strDataJson = JsonConvert.SerializeObject(body);
        HttpRequestMessage req = new(
            HttpMethod.Post,
            path
        )
        {
            Content = new StringContent(strDataJson, Encoding.UTF8, "application/json"),
        };

        HttpResponseMessage res = await _httpClient.SendAsync(req);
        string result = await res.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task<T?> Patch<T>(
        string path,
        object? body = null
    )
    {
        string strDataJson = JsonConvert.SerializeObject(body);
        HttpRequestMessage req = new(
            HttpMethod.Patch,
            path
        )
        {
            Content = new StringContent(strDataJson, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage res = await _httpClient.SendAsync(req);
        string result = await res.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(result);
    }
}
