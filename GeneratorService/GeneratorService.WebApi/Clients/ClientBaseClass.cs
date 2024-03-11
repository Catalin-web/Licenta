using System.Net;
using System.Text.Json;

namespace GeneratorService.WebApi.Clients
{
    public class ClientBaseClass
    {
        private HttpClient _httpClient;

        public ClientBaseClass(HttpClient httpClient)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            _httpClient = httpClient;
        }

        public async Task<TResponse> PostAsync<TResponse>(object entity, string route)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(entity);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + route, httpContent);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TResponse>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("HTTP Request Exception: " + e.Message);
            }
        }

        public async Task<string> PostAsyncAndReturnString(object entity, string route)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(entity);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + route, httpContent);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("HTTP Request Exception: " + e.Message);
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string route)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(route);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("HTTP Request Exception: " + e.Message);
            }
        }
    }
}
