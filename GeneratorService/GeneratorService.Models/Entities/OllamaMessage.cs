namespace GeneratorService.Models.Entities
{
    public class OllamaMessage
    {
        public string Content {  get; set; }
        public string Role { get; set; } = "user";
    }
}
