using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;

namespace ERP.Services.Services
{
    public class AuthenticationService : BaseService<SiteUserDto>, IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private IUserRepository _userRepository;
        private IUserService _userService;

        public AuthenticationService(IUserRepository userRepository, JwtSettings jwtSettings, IUserService userService) : base(userRepository)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
            _userService = userService;
        }

        private async Task<Claim[]> GenerateClaims(SiteUserDto UserDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,_jwtSettings.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("Id",UserDto.Id.ToString()),
                new Claim("FirstName",UserDto.FirstName),
                new Claim("LastName",UserDto.LastName),
                new Claim(ClaimTypes.Email,UserDto.Email),
                new Claim(ClaimTypes.Role, await _userService.GetUserRoleAsString(UserDto.Id))
            };

            return claims;
        }

        private async Task<JwtSecurityToken> GenerateJWT(SiteUserDto userDto)
        {
            var claims = await GenerateClaims(userDto);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return token;
        }


        public async Task<string> AuthenticateAccountAsync(string email, string password,bool passwordHashed)
        {
            SiteUserDto userDto = await _userRepository.GetUserWithEmailAsync(email);
            if (userDto == null || userDto.IsDeleted)
            {
                throw new KeyNotFoundException();
            }
            else if (!passwordHashed && !BCrypt.Net.BCrypt.Verify(password, userDto.Password))
            {
                throw new ArgumentException();
            }

            JwtSecurityToken token = await GenerateJWT(userDto);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
