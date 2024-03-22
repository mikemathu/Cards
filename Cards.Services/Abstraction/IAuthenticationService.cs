using Cards.Services.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Cards.Services.Abstraction
{
    public interface IAuthenticationService
    {
        public abstract Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistrationDto);
        public abstract ValueTask<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthenticationDto);
        public abstract Task<string> CreateToken();
    }
}
