using Fileservice.Models.Entities;
using Fileservice.Models.Requests;
using Fileservice.WebApi.Services.NotebookFacade;
using Microsoft.AspNetCore.Mvc;

namespace Fileservice.WebApi.Controllers
{
    [ApiController]
    [Route("fileService/notebook")]
    public class NotebookTagController : ControllerBase
    {
        private readonly INotebookFacade _notebookFacade;
        public NotebookTagController(INotebookFacade notebookFacade)
        {
            _notebookFacade = notebookFacade;
        }

        /// <summary>
        /// Query notebooks.
        /// </summary>
        /// <response code="200">Query notebooks.</response>
        [HttpPost]
        [Route("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Notebook>>> QueryNotebooksAsync(QueryNotebookRequest request)
        {
            try
            {
                return Ok(await _notebookFacade.QueryNotebooksAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Add notebook tag.
        /// </summary>
        /// <response code="200">Add notebook tag.</response>
        [HttpPost]
        [Route("tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notebook>> AddNotebookTag(AddNotebookTagRequest request)
        {
            try
            {
                var notebook = await _notebookFacade.AddTagToNotebookAsync(request);
                if (notebook == null)
                {
                    return Ok();
                }
                return Ok(notebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete notebook tag.
        /// </summary>
        /// <response code="200">Delete notebook tag.</response>
        [HttpDelete]
        [Route("tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notebook>> DeleteNotebookTag(DeleteNotebookTagRequest request)
        {
            try
            {
                var notebook = await _notebookFacade.DeleteTagFromNotebookAsync(request);
                if (notebook == null)
                {
                    return Ok();
                }
                return Ok(notebook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <response code="200">Get all tags.</response>
        [HttpGet]
        [Route("tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllTagsAsync()
        {
            try
            {
                return Ok(await _notebookFacade.GetAllTagsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
