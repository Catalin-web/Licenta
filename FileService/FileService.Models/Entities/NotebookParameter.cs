namespace Fileservice.Models.Entities
{
    public class NotebookParameter
    {
        public string NotebookParameterName { get; set; }
        public string NotebookParameterDescriptionOrValue { get; set; }
        public NotebookParameterType NotebookParameterType { get; set; }
    }
}
