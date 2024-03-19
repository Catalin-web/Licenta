using GeneratorService.Models.Entities.Jobs;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;
using GeneratorService.WebApi.Services.GenerateParameters;
using GeneratorService.WebApi.Services.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorService.WebApi.Controllers
{
    [ApiController]
    [Route("generatorService/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly IPullJobFacade _pullJobFacade;
        public JobsController(IPullJobFacade pullJobFacade)
        {
            _pullJobFacade = pullJobFacade;
        }

        /// <summary>
        /// Pull a model.
        /// </summary>
        /// <response code="200">Pull a model.</response>
        [HttpPost]
        [Route("models/pull")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PullJob>> PullModelAsync(OllamaPullModelRequest request)
        {
            try
            {
                return Ok(await _pullJobFacade.InsertAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all jobs.
        /// </summary>
        /// <response code="200">Get all jobs.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PullJob>>> GetAllJobsAsync()
        {
            try
            {
                return Ok(await _pullJobFacade.GetAllPullJobsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
