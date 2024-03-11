
using GeneratorService.Models.Entities;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;
using System.Net.Http;

namespace GeneratorService.WebApi.Clients.Ollama
{
    public class OllamaClient : ClientBaseClass, IOllamaClient
    {
        private readonly string PullRoute = "api/pull";
        private readonly string AllModelsRoute = "api/tags";
        private readonly string GenerateRoute = "api/generate";
        private readonly ILogger<OllamaClient> _logger;

        public OllamaClient(ILogger<OllamaClient> logger, HttpClient httpClient) : base(httpClient)
        {
            _logger = logger;
        }

        public Task<ListModelsResponse> GetAllInstalledModelsAsync()
        {
            return GetAsync<ListModelsResponse>(AllModelsRoute);
        }

        public async Task<string> PullModelAsync(OllamaPullModelRequest request)
        {
            return await PostAsyncAndReturnString(request, PullRoute);
        }

        public async Task<OllamaGenerateResponse> GenerateResponse(OllamaGenerateRequest request)
        {
            return await PostAsync<OllamaGenerateResponse>(request, GenerateRoute);
        }
    }
}
