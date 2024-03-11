namespace GeneratorService.Models.Responses
{
    public class OllamaGenerateResponse
    {
        public string Model { get; set; }
        public string Response { get; set; }
        public bool Done { get; set; }
    }
}
