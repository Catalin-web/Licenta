using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.WebApi.Services.NotebookGraphFacade;

namespace NotebookService.WebApi.Services
{
    public class StartingGraphBackgroundService : BackgroundService
    {
        private readonly ILogger<StartingGraphBackgroundService> _logger;
        private readonly INotebookNodeFacade _notebookNodeFacade;
        public StartingGraphBackgroundService(INotebookNodeFacade notebookNodeFacade, ILogger<StartingGraphBackgroundService> logger)
        {
            _notebookNodeFacade = notebookNodeFacade;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var graphs = await _notebookNodeFacade.GetAllStartingNotebookNodes();
                if (graphs.Count() == 0)
                {
                    var notebookNode = new NotebookNodeModel()
                    {
                        NotebookName = "ParentNotebook",
                        InputParameters = new List<NotebookParameter> { new NotebookParameter { Name = "ParentName", Value = "ParentValue" } },
                        InputParameterstoGenerate = new List<NotebookParameterToGenerate> { new NotebookParameterToGenerate { NameOfTheParameter = "ParentGenerateName", DescriptionOfTheParameter = "DescriptionParentGenerateName", ModelType = ModelType.OPEN_SOURCE } },
                        OutputParametersNames = new List<string> { "ParentName" },
                        ChildNodes = new List<NotebookNodeModel>
                        {
                            new NotebookNodeModel()
                            {
                                NotebookName = "Child1Notebook",
                                InputParameters = new List<NotebookParameter> { new NotebookParameter { Name = "Child1Name", Value = "Child1Value" } },
                                InputParameterstoGenerate = new List<NotebookParameterToGenerate> { new NotebookParameterToGenerate { NameOfTheParameter = "Child1GenerateName", DescriptionOfTheParameter = "DescriptionChild1GenerateName", ModelType = ModelType.OPEN_SOURCE } },
                                OutputParametersNames = new List<string> { "Child1Name" },
                            },
                            new NotebookNodeModel()
                            {
                                NotebookName = "Child2Notebook",
                                InputParameters = new List<NotebookParameter> { new NotebookParameter { Name = "Child2Name", Value = "Child2Value" } },
                                InputParameterstoGenerate = new List<NotebookParameterToGenerate> { new NotebookParameterToGenerate { NameOfTheParameter = "Child2GenerateName", DescriptionOfTheParameter = "DescriptionChild2GenerateName", ModelType = ModelType.OPEN_SOURCE } },
                                OutputParametersNames = new List<string> { "Child2Name" },
                                ChildNodes = new List<NotebookNodeModel>
                                {
                                    new NotebookNodeModel()
                                    {
                                        NotebookName = "Child2Child1Notebook",
                                        InputParameters = new List<NotebookParameter> { new NotebookParameter { Name = "Child2Child1Name", Value = "Child2Child1Value" } },
                                        InputParameterstoGenerate = new List<NotebookParameterToGenerate> { new NotebookParameterToGenerate { NameOfTheParameter = "Child2Child1GenerateName", DescriptionOfTheParameter = "DescriptionChild2Child1GenerateName", ModelType = ModelType.OPEN_SOURCE } },
                                        OutputParametersNames = new List<string> { "Child2Child1Name" },
                                    }
                                }
                            }
                        }
                    };
                    await _notebookNodeFacade.CreateNotebookGraph(notebookNode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
