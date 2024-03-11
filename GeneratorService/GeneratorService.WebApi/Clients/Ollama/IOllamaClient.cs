using GeneratorService.Models.Entities;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;

namespace GeneratorService.WebApi.Clients.Ollama
{
    public interface IOllamaClient
    {
        Task<string> PullModelAsync(OllamaPullModelRequest request);
        Task<ListModelsResponse> GetAllInstalledModelsAsync();
        Task<OllamaGenerateResponse> GenerateResponse(OllamaGenerateRequest request);
    }
}
