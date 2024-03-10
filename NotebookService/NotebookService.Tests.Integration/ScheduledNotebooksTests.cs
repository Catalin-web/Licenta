using NotebookService.Clients;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;

namespace NotebookService.Tests.Integration
{
    public class ScheduledNotebooksTests
    {
        private static ScheduledNotebookServiceClient _scheduledNotebookClient = new ScheduledNotebookServiceClient();

        [Fact]
        public async Task ScheduleNotebookWithName_ReturnsAValidScheduledNotebook()
        {
            var notebookName = $"Test-{Guid.NewGuid()}";

            var scheduledNotebook = await CreateNotebookWithName(notebookName);

            Assert.NotNull(scheduledNotebook);   
            Assert.Equal(notebookName, scheduledNotebook.NotebookName);
            Assert.Empty(scheduledNotebook.InputParameters);
            Assert.Empty(scheduledNotebook.OutputParametersNames);
            Assert.Empty(scheduledNotebook.OutputParameters);
            Assert.False(string.IsNullOrEmpty(scheduledNotebook.Id));
        }

        [Fact]
        public async Task HavingAScheduledNotebookWithName_GetScheduledNotebookReturnsPreviouslyCreateScheduledNotebook()
        {
            var notebookName = $"Test-{Guid.NewGuid()}";

            await CreateNotebookWithName(notebookName);
            var scheduledNotebook = (await _scheduledNotebookClient.GetAllScheduledNotebookAsync()).First(notebook => notebook.NotebookName == notebookName);

            Assert.NotNull(scheduledNotebook);
            Assert.Equal(notebookName, scheduledNotebook.NotebookName);
            Assert.Empty(scheduledNotebook.InputParameters);
            Assert.Empty(scheduledNotebook.OutputParametersNames);
            Assert.Empty(scheduledNotebook.OutputParameters);
            Assert.False(string.IsNullOrEmpty(scheduledNotebook.Id));
        }

        [Fact]
        public async Task HavingAScheduledNotebookWithName_FinishScheduledNotebook()
        {
            var notebookName = $"Test-{Guid.NewGuid()}";

            var scheduldeNotebook = await CreateNotebookWithName(notebookName);
            var finishScheduledNotebook = new FinishScheduledNotebookRequest()
            {
                ScheduledNotebookId = scheduldeNotebook.Id,
                Status = Status.SUCCEDED,
                OutputParameters = new List<NotebookParameter>()
            };
            var scheduledNotebookBefore = (await _scheduledNotebookClient.GetAllScheduledNotebookAsync()).FirstOrDefault(scheduledNotebook => scheduledNotebook.NotebookName == notebookName);
            var finishedScheduledNotebook = await _scheduledNotebookClient.FinishScheduledNotebookAsync(finishScheduledNotebook);
            var scheduledNotebookAfter = (await _scheduledNotebookClient.GetAllScheduledNotebookAsync()).FirstOrDefault(scheduledNotebook => scheduledNotebook.NotebookName == notebookName);
            var historyOfScheduledNotebook = (await _scheduledNotebookClient.GetAllHistoryOfScheduledNotebook()).FirstOrDefault(scheduledNotebook => scheduledNotebook.NotebookName == notebookName);

            Assert.NotNull(finishedScheduledNotebook);
            Assert.Equal(notebookName, finishedScheduledNotebook.NotebookName);
            Assert.Empty(finishedScheduledNotebook.InputParameters);
            Assert.Empty(finishedScheduledNotebook.OutputParametersNames);
            Assert.Empty(finishedScheduledNotebook.OutputParameters);
            Assert.False(string.IsNullOrEmpty(finishedScheduledNotebook.Id));
            Assert.Equal(Status.SUCCEDED, finishedScheduledNotebook.Status);
            Assert.NotNull(scheduledNotebookBefore);
            Assert.Null(scheduledNotebookAfter);
            Assert.NotNull(historyOfScheduledNotebook);
            Assert.Equal(notebookName,historyOfScheduledNotebook.NotebookName);
        }

        [Fact]
        public async Task HavingAScheduledNotebookWithName_UpdateProgressOfScheduledNotebook()
        {
            var notebookName = $"Test-{Guid.NewGuid()}";

            var scheduldeNotebook = await CreateNotebookWithName(notebookName);
            var updateProgressRequest = new UpdateProgressOfScheduledNotebookRequest()
            {
                ScheduledNotebookId = scheduldeNotebook.Id,
                Progress = Progress.COMPLETED
            };
            var updatedScheduledNotebook = await _scheduledNotebookClient.UpdateProgressOfScheaduledNotebookAsync(updateProgressRequest);

            Assert.NotNull(updatedScheduledNotebook);
            Assert.Equal(notebookName, updatedScheduledNotebook.NotebookName);
            Assert.Empty(updatedScheduledNotebook.InputParameters);
            Assert.Empty(updatedScheduledNotebook.OutputParametersNames);
            Assert.Empty(updatedScheduledNotebook.OutputParameters);
            Assert.False(string.IsNullOrEmpty(updatedScheduledNotebook.Id));
            Assert.Equal(Progress.COMPLETED, updatedScheduledNotebook.Progress);
        }

        private async Task<ScheduledNotebook> CreateNotebookWithName(string notebookName)
        {
            ScheduleNotebookRequest request = new ScheduleNotebookRequest()
            {
                NotebookName = notebookName,
                InputParameters = new List<NotebookParameter> { },
                OutputParametersNames = new List<string> { }
            };

            return await _scheduledNotebookClient.ScheduleNotebookAsync(request);
        }
    }
}
