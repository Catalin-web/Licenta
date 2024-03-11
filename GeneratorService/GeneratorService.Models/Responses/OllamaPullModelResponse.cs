using GeneratorService.Models.Entities;

namespace GeneratorService.Models.Responses
{
    public class OllamaPullModelResponse
    {
        public IEnumerable<PullMessage> PullMessages { get; set; }

        public bool Succeded { get; set; }
    }
}
