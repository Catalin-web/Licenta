using Microsoft.AspNetCore.Mvc;
using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.WebApi.Services.NotebookGraphFacade;

namespace NotebookService.WebApi.Controllers
{
    [ApiController]
    [Route("notebookService/notebookGraph")]
    public class NotebookGraphController : ControllerBase
    {
        private readonly INotebookGraphFacade _notebookGraphFacade;
        public NotebookGraphController(INotebookGraphFacade notebookGraphFacade)
        {
            _notebookGraphFacade = notebookGraphFacade;
        }

        /// <summary>
        /// Create a notebook graph.
        /// </summary>
        /// <response code="200">Create a notebook graph.</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NotebookGraph>> CreateNotebookGraphAsync(CreateNotebookGraphRequest request)
        {
            try
            {
                var notebookGraph = await _notebookGraphFacade.CreateNotebookGraph(request);
                return Ok(notebookGraph);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all notebook graphs.
        /// </summary>
        /// <response code="200">Get all notebook graphs..</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NotebookGraph>>> GetAllNotebookGraphsAsync()
        {
            try
            {
                return Ok(await _notebookGraphFacade.GetAllNotebookGraph());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Schedule a notebook graph.
        /// </summary>
        /// <response code="200">Schedule a notebook graph.</response>
        [HttpPost]
        [Route("schedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScheduledNotebook>> ScheduleNotebookGraphAsync(ScheduleNotebookGraphRequest request)
        {
            try
            {
                return Ok(await _notebookGraphFacade.ScheduleNotebookGraph(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
