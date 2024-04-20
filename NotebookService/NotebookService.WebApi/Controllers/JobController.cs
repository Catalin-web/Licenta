using Microsoft.AspNetCore.Mvc;
using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests.Jobs;
using NotebookService.WebApi.Services.Jobs;

namespace NotebookService.WebApi.Controllers
{
    [ApiController]
    [Route("notebookService/jobs")]
    public class JobController : ControllerBase
    {
        private readonly INotebookJobFacade _jobFacade;
        private readonly INotebookGraphJobFacade _notebookGraphJobFacade;
        public JobController(INotebookJobFacade jobFacade, INotebookGraphJobFacade notebookGraphJobFacade)
        {
            _jobFacade = jobFacade;
            _notebookGraphJobFacade = notebookGraphJobFacade;
        }

        /// <summary>
        /// Schedule an execution.
        /// </summary>
        /// <response code="200">Schedule execution.</response>
        [HttpPost]
        [Route("notebook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ShceduleNotebookJob(NotebookScheduleJobRequest request)
        {
            try
            {
                return Ok(await _jobFacade.ScheduleNotebookJob(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all scheduled jobs for a specific user.
        /// </summary>
        /// <response code="200">Get all scheduled jobs for a specific user.</response>
        [HttpGet]
        [Route("notebook/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookJobModel>>> GetAllTriggerNotebookJobsForUser(string userId)
        {
            try
            {
                return Ok(await _jobFacade.GetAllScheduledNotebookJobForUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all scheduled jobs.
        /// </summary>
        /// <response code="200">Get all scheduled jobs.</response>
        [HttpGet]
        [Route("notebook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookJobModel>>> GetAllTriggerNotebookJobs()
        {
            try
            {
                return Ok(await _jobFacade.GetAllScheduledNotebookJob());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific scheduled notebook job.
        /// </summary>
        /// <response code="200">Delete a specific scheduled notebook job.</response>
        [HttpDelete]
        [Route("notebook/{triggerNotebookJobId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookJobModel>>> DeleteTriggerNotebook(string triggerNotebookJobId)
        {
            try
            {
                return Ok(await _jobFacade.DeleteTriggerNotebookJob(triggerNotebookJobId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get trigger history for a specific trigger notebook job.
        /// </summary>
        /// <response code="200">Get trigger history for a specific trigger notebook job.</response>
        [HttpGet]
        [Route("notebook/history/{triggerNotebookJobId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookJobHistoryModel>>> GetTriggerNotebookJobHistory(string triggerNotebookJobId)
        {
            try
            {
                return Ok(await _jobFacade.GetHistoryForTriggerjob(triggerNotebookJobId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Schedule a notebook graph.
        /// </summary>
        /// <response code="200">Schedule a notebook graph</response>
        [HttpPost]
        [Route("notebookGraph")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ShceduleNotebookGraphJob(NotebookGraphScheduleJobRequest request)
        {
            try
            {
                return Ok(await _notebookGraphJobFacade.ScheduleNotebookGraphJob(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all scheduled jobs for a specific user.
        /// </summary>
        /// <response code="200">Get all scheduled jobs for a specific user.</response>
        [HttpGet]
        [Route("notebookGraph/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookGraphJobModel>>> GetAllTriggerNotebookGraphJobsForUser(string userId)
        {
            try
            {
                return Ok(await _notebookGraphJobFacade.GetAllScheduledNotebookGraphJobForUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all scheduled jobs.
        /// </summary>
        /// <response code="200">Get all scheduled jobs.</response>
        [HttpGet]
        [Route("notebookGraph")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookGraphJobModel>>> GetAllTriggerNotebookGraphJobs()
        {
            try
            {
                return Ok(await _notebookGraphJobFacade.GetAllScheduledNotebookGraphJob());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific scheduled notebook graph job.
        /// </summary>
        /// <response code="200">Delete a specific scheduled notebook job.</response>
        [HttpDelete]
        [Route("notebookGraph/{triggerNotebookGraphJobId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookGraphJobModel>>> DeleteTriggerNotebookGraph(string triggerNotebookGraphJobId)
        {
            try
            {
                return Ok(await _notebookGraphJobFacade.DeleteTriggerNotebookGraphJob(triggerNotebookGraphJobId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get trigger history for a specific trigger notebook graph job.
        /// </summary>
        /// <response code="200">Get trigger history for a specific trigger notebook graph job.</response>
        [HttpGet]
        [Route("notebookGraph/history/{triggerNotebookGraphJobId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TriggerNotebookGraphJobHistoryModel>>> GetTriggerNotebookGraphJobHistory(string triggerNotebookGraphJobId)
        {
            try
            {
                return Ok(await _notebookGraphJobFacade.GetHistoryForNotebookGraphJob(triggerNotebookGraphJobId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
