using GeneratorService.Models.Entities;

namespace GeneratorService.Models.Responses
{
    public class ListModelsResponse
    {
        public IEnumerable<OpenSourceModel> Models { get; set; }
    }
}
