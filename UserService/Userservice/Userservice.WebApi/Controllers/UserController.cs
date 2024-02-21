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
        [Route("user")]
        [Produces(MediaTypeNames.Application.Json)]
        public class UserController
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
            [Route("create")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<User> CreateUser(RegisterRequest request)
            {
                return await _userFacade.Register(request);
            }

            /// <summary>
            /// Login.
            /// </summary>
            /// <response code="200">Login.</response>
            [HttpPost]
            [Route("get")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<User> Login(LoginRequest request)
            {
                return await _userFacade.Login(request);
            }

            /// <summary>
            /// Get a user by id.
            /// </summary>
            /// <response code="200">Get a user by id.</response>
            [HttpPost]
            [Route("get/{userId}")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<User> GetUserById(string userId)
            {
                return await _userFacade.GetUserById(userId);
            }
        }
    }
}
