using System.Globalization;
using System.Net;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace FileService.Clients
{
    public class ClientBaseClass
    {
        private HttpClient _httpClient;

        public ClientBaseClass()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:12600");
        }

        public async Task<TResponse> PostAsync<TResponse>(object entity, string route)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(entity);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + route, httpContent);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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

        public async Task<byte[]> DownloadAsync(string route)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(route);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("HTTP Request Exception: " + e.Message);
            }
        }

        public async Task<TResponse> PostAsync<TResponse>(string route, byte[] file)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(new MemoryStream(file)), "file", "file");

                using (var message = await _httpClient.PostAsync(route, content))
                {
                    var jsonResponse = await message.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<TResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
