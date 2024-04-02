using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Requests.NotebookGraph
{
    public class CreateNotebookNodeRequest
    {
        public string NotebookName { get; set; }
        public IEnumerable<NotebookParameter> InputParameters { get; set; }
        public IEnumerable<NotebookParameterToGenerate> InputParameterstoGenerate { get; set; }
        public IEnumerable<string> OutputParametersNames { get; set; }
    }
}
