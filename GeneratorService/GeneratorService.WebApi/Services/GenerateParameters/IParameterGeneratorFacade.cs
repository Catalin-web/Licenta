using GeneratorService.Models.Entities;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;

namespace GeneratorService.WebApi.Services.GenerateParameters
{
    public interface IParameterGeneratorFacade
    {
        Task<ParameterGeneratorResponse> GenerateParameterAsync(ParameterGeneratorRequest request);
        Task<OllamaPullModelResponse> PullModelAsync(OllamaPullModelRequest request);
        Task<ListModelsResponse> GetAllPulledModelsAsync();
        Task<OllamaGenerateResponse> GenerateResponse(OllamaGenerateRequest request);
    }
}
