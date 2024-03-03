using Microsoft.AspNetCore.Mvc;
using NotebookService.Models.Entities;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;
using NotebookService.WebApi.Services.ScheduleNotebookFacade;

namespace NotebookService.WebApi.Controllers
{
    [ApiController]
    [Route("notebookService/scheduleNotebook")]
    public class ScheduleNotebookController : ControllerBase
    {
        private readonly IScheduleNotebookFacade _scheduleNotebookFacade;
        public ScheduleNotebookController(IScheduleNotebookFacade scheduleNotebookFacade)
        {
            _scheduleNotebookFacade = scheduleNotebookFacade;
        }

        /// <summary>
        /// Schedule notebook.
        /// </summary>
        /// <response code="200">Schedule notebook.</response>
        [HttpPost]
        [Route("schedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScheduledNotebook>> ScheduleNotebook(ScheduleNotebookRequest scheduleNotebookRequest)
        {
            var scheduledNotebook = await _scheduleNotebookFacade.ScheduleNotebook(scheduleNotebookRequest);
            return Ok(scheduledNotebook);
        }

        /// <summary>
        /// Get all scheduled notebooks.
        /// </summary>
        /// <response code="200">Get all scheduled notebooks.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ScheduledNotebook>>> GetAllScheduledNotebooks()
        {
            return Ok(await _scheduleNotebookFacade.GetAllScheduledNotebooks());
        }

        /// <summary>
        /// Finish execution of a notebook.
        /// </summary>
        /// <response code="200">Finish execution of a notebooks.</response>
        [HttpPost]
        [Route("finish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScheduledNotebook>> FinishScheduledExecution(FinishScheduledNotebookRequest finishScheduledNotebookRequest)
        {
            try
            {
                return Ok(await _scheduleNotebookFacade.FinishScheduledNotebook(finishScheduledNotebookRequest));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update progress of a scheduled notebook.
        /// </summary>
        /// <response code="200">Update progress of a scheduled notebook.</response>
        [HttpPost]
        [Route("updateStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScheduledNotebook>> UpdateProgressOfAScheduledNotebook(UpdateProgressOfScheduledNotebookRequest updateProgressOfScheduledNotebookRequest)
        {
            try
            {
                return Ok(await _scheduleNotebookFacade.UpdateProgressOfScheduledNotebook(updateProgressOfScheduledNotebookRequest));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}