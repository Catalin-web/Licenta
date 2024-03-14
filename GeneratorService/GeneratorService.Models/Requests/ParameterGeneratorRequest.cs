namespace GeneratorService.Models.Requests
{
    public class ParameterGeneratorRequest
    {
        public string NameOfTheParameter { get; set; }
        public string DescriptionOfTheParameter { get; set; }
        public ModelType ModelType { get; set; }
    }
}
