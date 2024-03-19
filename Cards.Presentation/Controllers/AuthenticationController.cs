using Cards.Presentation.Filters;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [ValidateModel]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            await _authenticationService.RegisterUserAsync(userForRegistrationDto);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            bool isUserValid = await _authenticationService.ValidateUserAsync(userForAuthenticationDto);

            if (!isUserValid)
                return Unauthorized();

            string token =await  _authenticationService.CreateToken();

            return Ok(new { Token = token });
        }
    }
}
