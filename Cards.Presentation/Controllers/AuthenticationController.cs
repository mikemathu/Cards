using Cards.Presentation.Filters;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;
using Microsoft.AspNetCore.Http;
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
        /// <remarks>
        /// Sample request:
        /// POST /api/registerUser
        /// {
        ///     "username": "example",
        ///     "password": "password123",
        ///     "email": "example@example.com"
        /// }
        /// </remarks>
        /// <returns>HTTP status code 201 (Created) if successful.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            await authenticationService.RegisterUserAsync(userForRegistrationDto);

            return StatusCode(201);
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="userForAuthenticationDto">User authentication data.</param>
        /// <remarks>
        /// Sample request:
        /// POST /api/login
        /// {
        ///     "username": "example",
        ///     "password": "password123"
        /// }
        /// </remarks>
        /// <returns>
        /// HTTP status code 200 (OK) with authentication token if successful,
        /// HTTP status code 401 (Unauthorized) if authentication fails.
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
