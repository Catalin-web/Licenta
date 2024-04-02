using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Entities.NotebookGraph
{
    public class NotebookNodeModel
    {
        public string NotebookName { get; set; }
        public IEnumerable<NotebookParameter> InputParameters { get; set; }
        public IEnumerable<NotebookParameterToGenerate> InputParameterstoGenerate { get; set; }
        public IEnumerable<string> OutputParametersNames { get; set; }
        public IEnumerable<NotebookNodeModel> ChildNodes { get; set; } = new List<NotebookNodeModel>();
    }
}
