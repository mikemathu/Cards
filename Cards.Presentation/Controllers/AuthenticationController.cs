using Cards.Presentation.Filters;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [ValidateModel]
    [Produces("application/json")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userForRegistrationDto">User registration data.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/authentication
        ///     {
        ///        "email": "user@gmail.com",
        ///        "password": "userP@ssorwd1"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">User successfully registered.</response>
        /// <response code="400">Bad Request: If password is less than 6 characters.</response>
        /// <response code="409">Conflict: If email address already in use.</response>
        /// <response code="422">Unprocessable Entity: If the request data is invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            IdentityResult result = await authenticationService.RegisterUserAsync(userForRegistrationDto);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="userForAuthenticationDto">User authentication data.</param>
        /// <returns>Returns a token</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/authentication/login
        ///     {
        ///         "email": "kev@gmail.com",
        ///         "password": "kevP@ssword1"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">User successfully authenticated. Returns a token.</response>
        /// <response code="401">Unauthorized: Invalid credentials.</response>
        /// <response code="422">Unprocessable Entity: If the required fields are missing in the request data.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            bool isUserValid = await authenticationService.ValidateUserAsync(userForAuthenticationDto);

            if (!isUserValid)
                return Unauthorized();

            string token =await  authenticationService.CreateToken();

            return Ok(new { Token = token });
        }

    }
}
