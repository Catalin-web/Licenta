namespace GeneratorService.Models.Requests
{
    public class OllamaGenerateRequest
    {
        public string Model { get; set; }
        public string Prompt { get; set; }
        public string System { get; set; }
        public bool Stream { get; set; } = false;
    }
}
