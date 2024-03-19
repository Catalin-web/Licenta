using GeneratorService.Models.Entities;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;
using GeneratorService.WebApi.Clients.Ollama;
using GeneratorService.WebApi.Settings;
using System.Text.Json;

namespace GeneratorService.WebApi.Services.GenerateParameters
{
    public class ParameterGeneratorFacade : IParameterGeneratorFacade
    {
        private readonly IOllamaClient _ollamaClient;
        private readonly string _defaultOpenSourceModelToGenerateParameters;

        public ParameterGeneratorFacade(IOllamaClient ollamaClient, ISettingsProvider settingsProvider)
        {
            _ollamaClient = ollamaClient;
            _defaultOpenSourceModelToGenerateParameters = settingsProvider.DefaultOpenSourceModelToGenerateParameters;
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
            var ollamaGenerateRequest = new OllamaGenerateRequest()
            {
                Model = $"{_defaultOpenSourceModelToGenerateParameters}:latest",
                System = GeneratingParametersPrompts.SystemPromptToGenerateParameters,
                Prompt = GeneratingParametersPrompts.UserPromptToGenerateParameters.Replace("NAME", request.NameOfTheParameter).Replace("DESCRIPTION", request.DescriptionOfTheParameter),
                Stream = false
            };
            var ollamaGenerateResponse = await _ollamaClient.GenerateResponse(ollamaGenerateRequest);
            return new ParameterGeneratorResponse()
            {
                Value = ollamaGenerateResponse.Response
            };
        }

        public async Task<OllamaGenerateResponse> GenerateResponse(OllamaGenerateRequest request)
        {
            return await _ollamaClient.GenerateResponse(request);
        }
    }
}
