using GeneratorService.Models.Entities;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;
using GeneratorService.WebApi.Clients.Ollama;
using System.Text.Json;

namespace GeneratorService.WebApi.Services.GenerateParameters
{
    public class ParameterGeneratorFacade : IParameterGeneratorFacade
    {
        private readonly IOllamaClient _ollamaClient;
        public ParameterGeneratorFacade(IOllamaClient ollamaClient)
        {
            _ollamaClient = ollamaClient;
        }

        public async Task<OllamaPullModelResponse> PullModelAsync(OllamaPullModelRequest request)
        {
            var allMesages = (await _ollamaClient.PullModelAsync(request)).Split("\n").Where(message => !string.IsNullOrEmpty(message));
            var pullMessages = new List<PullMessage>();
            foreach (var messages in allMesages)
            {
                var pullMessage = JsonSerializer.Deserialize<PullMessage>(messages, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                pullMessages.Add(pullMessage);
            }
            return new OllamaPullModelResponse()
            {
                PullMessages = pullMessages,
                Succeded = pullMessages.All(pullMesage => string.IsNullOrEmpty(pullMesage.Error))
            };
        }

        public async Task<ListModelsResponse> GetAllPulledModelsAsync()
        {
            return await _ollamaClient.GetAllInstalledModelsAsync();
        }

        public async Task<ParameterGeneratorResponse> GenerateParameterAsync(ParameterGeneratorRequest request)
        {
            return null;
        }

        public async Task<OllamaGenerateResponse> GenerateResponse(OllamaGenerateRequest request)
        {
            return await _ollamaClient.GenerateResponse(request);
        }
    }
}
