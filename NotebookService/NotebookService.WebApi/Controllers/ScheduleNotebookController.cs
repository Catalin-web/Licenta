using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var scheduledNotebook = await _scheduleNotebookFacade.ScheduleNotebook(scheduleNotebookRequest);
                return Ok(scheduledNotebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                return Ok(await _scheduleNotebookFacade.GetAllScheduledNotebooks());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                return BadRequest(ex);
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
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get all history of scheduled notebooks
        /// </summary>
        /// <response code="200">Get all history of scheduled notebooks.</response>
        [HttpGet]
        [Route("history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ScheduledNotebook>>> GetAllHistoryOfScheduledNotebook()
        {
            try
            {
                return Ok(await _scheduleNotebookFacade.GetAllHistoryOfScheduledNotebook());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}