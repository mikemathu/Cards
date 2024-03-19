using AutoMapper;
using Cards.Domain.Entities;
using Cards.Domain.Exceptions;
using Cards.Services.Abstraction;
using Cards.Services.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cards.Services.Services
{
    public class AuthenticationService(UserManager<AppUser> userManager, IConfiguration configuration, IMapper mapper) : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;

        private AppUser? _appUser;

        public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration)
        {
            AppUser appUser = _mapper.Map<AppUser>(userForRegistration);

            AppUser? userExist = await _userManager.FindByEmailAsync(userForRegistration.Email);

            if (userExist != null)
                throw new EmailAlreadyExistsException();

            IdentityResult result = await _userManager.CreateAsync(appUser, userForRegistration.Password);

            if (!result.Succeeded)
                throw new CreateUserFailedException();

            return result;
        }

        public async ValueTask<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthenticationDto)
        {
            _appUser = await _userManager.FindByEmailAsync(userForAuthenticationDto.Email);

            bool isPasswordValid = false;

            if (_appUser != null)
                isPasswordValid = await _userManager.CheckPasswordAsync(_appUser, userForAuthenticationDto.Password);

            return isPasswordValid;
        }

        public string CreateToken()
        {
            SigningCredentials signingCredentials = GetSigningCredentials();

            List<Claim> claims = GetClaims();

            JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            string jwtTokenHandler = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return jwtTokenHandler;
        }

        private SigningCredentials GetSigningCredentials()
        {
            //var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Secret").Value);
            byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CardAPISecret"));


            SymmetricSecurityKey key = new(secret);

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims()
        {
            List<Claim> claims = [];

            if (_appUser != null && _appUser.Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, _appUser.Email));
            }
            return claims;
        }


        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");

            JwtSecurityToken tokenOptions = new
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }

}
