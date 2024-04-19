using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Userservice.Models.Entities;
using Userservice.Models.Requests;
using Userservice.WebApi.Services.UserFacade;

namespace Userservice.WebApi.Controllers
{
    namespace Server.WebApi.Controllers
    {
        [ApiController]
        [Route("userService")]
        [Produces(MediaTypeNames.Application.Json)]
        public class UserController : ControllerBase
        {
            private readonly IUserFacade _userFacade;
            public UserController(IUserFacade userFacade)
            {
                _userFacade = userFacade;
            }

            /// <summary>
            /// Register.
            /// </summary>
            /// <response code="200">Register.</response>
            [HttpPost]
            [Route("register")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<User>> CreateUser(RegisterRequest request)
            {
                try
                {
                    return Ok(await _userFacade.Register(request));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            /// <summary>
            /// Login.
            /// </summary>
            /// <response code="200">Login.</response>
            [HttpPost]
            [Route("login")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<User>> Login(LoginRequest request)
            {
                try
                {
                    return Ok(await _userFacade.Login(request));
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            /// <summary>
            /// Get a user by id.
            /// </summary>
            /// <response code="200">Get a user by id.</response>
            [HttpGet]
            [Route("user/{userId}")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<User>> GetUserById(string userId)
            {
                try
                {
                    return Ok(await _userFacade.GetUserById(userId));
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
