using Fileservice.Models.Entities;
using Fileservice.WebApi.Services.NotebookFacade;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fileservice.WebApi.Controllers
{
    [ApiController]
    [Route("notebook")]
    public class NotebookController : ControllerBase
    {
        private readonly INotebookFacade _notebookFacade;
        public NotebookController(INotebookFacade notebookFacade)
        {
            _notebookFacade = notebookFacade;
        }

        /// <summary>
        /// Publish notebook.
        /// </summary>
        /// <response code="200">Publish notebook.</response>
        [HttpPost]
        [Route("upload/{notebookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NotebookWithParametersAndMetadata>> UploadNotebook(string notebookName, IFormFile file)
        {
            var notebookWithParametersAndMetadata = await _notebookFacade.UploadNotebook(notebookName, file);
            return Ok(notebookWithParametersAndMetadata);
        }

        /// <summary>
        /// Download notebook.
        /// </summary>
        /// <response code="200">Download notebook.</response>
        [HttpGet]
        [Route("download/{notebookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadNotebook(string notebookName)
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                var contentType = await _notebookFacade.DownloadNotebook(memoryStream, notebookName);
                return File(memoryStream.ToArray(), contentType, notebookName);
            }
        }

        /// <summary>
        /// Get all notebooks.
        /// </summary>
        /// <response code="200">Get all notebooks.</response>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NotebookWithParametersAndMetadata>>> GetAllNotebooks()
        {
            return Ok(await _notebookFacade.GetAllNotebooks());
        }

        /// <summary>
        /// Get all notebooks.
        /// </summary>
        /// <response code="200">Get all notebooks.</response>
        [HttpGet]
        [Route("name/{notebookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NotebookWithParametersAndMetadata>> GetNotebookByName(string notebookName)
        {
            var notebookWithParametersAndMetadata = await _notebookFacade.GetNotebookByName(notebookName);
            if (notebookWithParametersAndMetadata == null)
            {
                return Ok();
            }
            return Ok(notebookWithParametersAndMetadata);
        }
    }
}
