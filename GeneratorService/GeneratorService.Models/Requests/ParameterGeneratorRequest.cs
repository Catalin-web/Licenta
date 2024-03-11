namespace GeneratorService.Models.Requests
{
    public class ParameterGeneratorRequest
    {
        public string Name { get; set; }
        public string Prompt { get; set; }
        public ModelType ModelType { get; set; }
    }
}
