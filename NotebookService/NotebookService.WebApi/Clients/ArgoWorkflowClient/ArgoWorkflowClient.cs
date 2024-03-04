using NotebookService.Models.Entities.Argo;
using NotebookService.Models.Requests;
using NotebookService.WebApi.Clients.ArgoWorkflowClient;

namespace NotebookService.WebApi.Clients.Argo
{
    public class ArgoWorkflowClient : IArgoWorkflowClient
    {
        private string _workflowTemplatesPath = "api/v1/workflows/argo/submit";
        private HttpClient _httpClient;
        public ArgoWorkflowClient(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        public async Task ScheduleExecution(string scheduledNotebookId)
        {
            try
            {
                var submitOptions = new ArgoSubmitOptions()
                {
                    Parameters = new List<string>() { $"scheduled_notebook_id={scheduledNotebookId}" }
                };
                var submitArgoRequest = new SubmitArgoRequest()
                {
                    SubmitOptions = submitOptions
                };
                var response = await _httpClient.PostAsJsonAsync(_workflowTemplatesPath, submitArgoRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
