using Fileservice.Models.Entities;
using Fileservice.WebApi.Services.NotebookFacade;
using Microsoft.AspNetCore.Mvc;
using Minio.Exceptions;

namespace Fileservice.WebApi.Controllers
{
    [ApiController]
    [Route("fileService/notebook")]
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
        public async Task<ActionResult<Notebook>> UploadNotebook(string notebookName, IFormFile file)
        {
            try
            {
                var notebookWithParametersAndMetadata = await _notebookFacade.UploadNotebook(notebookName, file);
                return Ok(notebookWithParametersAndMetadata);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    var contentType = await _notebookFacade.DownloadNotebook(memoryStream, notebookName);
                    return File(memoryStream.ToArray(), contentType, notebookName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all notebooks.
        /// </summary>
        /// <response code="200">Get all notebooks.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetAllNotebooks()
        {
            try
            {
                return Ok(await _notebookFacade.GetAllNotebooks());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all notebooks.
        /// </summary>
        /// <response code="200">Get all notebooks.</response>
        [HttpGet]
        [Route("name/{notebookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notebook>> GetNotebookByName(string notebookName)
        {
            try
            {
                var notebookWithParametersAndMetadata = await _notebookFacade.GetNotebookByName(notebookName);
                if (notebookWithParametersAndMetadata == null)
                {
                    return Ok();
                }
                return Ok(notebookWithParametersAndMetadata);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
