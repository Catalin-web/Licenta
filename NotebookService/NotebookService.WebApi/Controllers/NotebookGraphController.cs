﻿using Microsoft.AspNetCore.Mvc;
using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.Models.Responses.NotebookGraph;
using NotebookService.WebApi.Services.NotebookGraphFacade;

namespace NotebookService.WebApi.Controllers
{
    [ApiController]
    [Route("notebookService/notebookGraph")]
    public class NotebookGraphController : ControllerBase
    {
        private readonly INotebookNodeFacade _notebookGraphFacade;
        public NotebookGraphController(INotebookNodeFacade notebookGraphFacade)
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
        public async Task<ActionResult<NotebookNode>> CreateStartingNotebookNodeAsync(CreateNotebookGraphRequest request)
        {
            try
            {
                var notebookGraph = await _notebookGraphFacade.CreateStartingNode(request);
                return Ok(notebookGraph);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add dependency between 2 nodes.
        /// </summary>
        /// <response code="200">Add dependency between 2 nodes.</response>
        [HttpPost]
        [Route("create/dependency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AddDependencyBetweenNodesResponse>> AddDependencyBetweenTwoNodesAsync(AddDependencyBetweenNodesRequest request)
        {
            try
            {
                return Ok(await _notebookGraphFacade.AddDependencyBetweenNotebookNodes(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all notebook nodes.
        /// </summary>
        /// <response code="200">Get all notebook nodes.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NotebookNode>>> GetAllNotebookNodes()
        {
            try
            {
                return Ok(await _notebookGraphFacade.GetAllNotebookNodes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Schedule a notebook node.
        /// </summary>
        /// <response code="200">Schedule a notebook node.</response>
        [HttpPost]
        [Route("schedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScheduledNotebook>> ScheduleNotebookNodeAsync(ScheduleNotebookNodeRequest request)
        {
            try
            {
                return Ok(await _notebookGraphFacade.ScheduleNotebookNode(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
