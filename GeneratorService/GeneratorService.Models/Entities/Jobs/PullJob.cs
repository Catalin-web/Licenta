using GeneratorService.Models.Responses;

namespace GeneratorService.Models.Entities.Jobs
{
    public class PullJob : JobBase
    {
        public string Model { get; set; }
        public OllamaPullModelResponse Response { get; set; }
    }
}
