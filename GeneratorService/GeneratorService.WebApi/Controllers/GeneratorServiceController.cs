using GeneratorService.Models.Entities;
using GeneratorService.Models.Requests;
using GeneratorService.Models.Responses;
using GeneratorService.WebApi.Services.GenerateParameters;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorService.WebApi.Controllers
{
    [ApiController]
    [Route("generatorService")]
    public class GeneratorServiceController : ControllerBase
    {
        private readonly IParameterGeneratorFacade _parameterGeneratorFacade;
        public GeneratorServiceController(IParameterGeneratorFacade parameterGeneratorFacade)
        {
            _parameterGeneratorFacade = parameterGeneratorFacade;
        }

        /// <summary>
        /// Pull a model.
        /// </summary>
        /// <response code="200">Pull a model.</response>
        [HttpPost]
        [Route("models/pull")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OllamaPullModelResponse>> PullModel(OllamaPullModelRequest request)
        {
            try
            {
                return Ok(await _parameterGeneratorFacade.PullModelAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// List all available models.
        /// </summary>
        /// <response code="200">List all available models.</response>
        [HttpGet]
        [Route("models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListModelsResponse>> GetAllAvailableModels()
        {
            try
            {
                var allAvailableModels = await _parameterGeneratorFacade.GetAllPulledModelsAsync();
                return Ok(allAvailableModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Generate a completion.
        /// </summary>
        /// <response code="200">Generate a completion.</response>
        [HttpPost]
        [Route("models/generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OllamaGenerateResponse>> GenerateResponseAsync(OllamaGenerateRequest request)
        {
            try
            {
                var ollamaGenerateResponse = await _parameterGeneratorFacade.GenerateResponse(request);
                return Ok(ollamaGenerateResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Generate a parameter.
        /// </summary>
        /// <response code="200">Generate a parameter.</response>
        [HttpPost]
        [Route("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ParameterGeneratorResponse>> ScheduleNotebook(ParameterGeneratorRequest parameterGeneratorRequest)
        {
            try
            {
                var parameterGeneratorResponse = await _parameterGeneratorFacade.GenerateParameterAsync(parameterGeneratorRequest);
                return Ok(parameterGeneratorResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
