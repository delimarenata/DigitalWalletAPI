using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.IServices;
using DigitalWalletAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace DigitalWalletAPI.Services
{

    public class ApiUserService : IApiUserService
    {
        private readonly IApiUserRepository _apiUserRepository;
        private readonly IConfiguration _configuration;

        public ApiUserService(IApiUserRepository apiUserRepository, IConfiguration configuration)
        {
            _apiUserRepository = apiUserRepository;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(ApiUserLoginDto loginDto)
        {
            var user = await _apiUserRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciais inválidas!");

            return GenerateJwtToken(user);
        }

        public async Task RegisterAsync(ApiUserDto userDto)
        {
            var userExists = await _apiUserRepository.GetByEmailAsync(userDto.Email);
            if (userExists != null)
                throw new Exception("Usuário já existe!");

            var apiUser = new ApiUser
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = userDto.Role
            };

            await _apiUserRepository.AddAsync(apiUser);
        }

        private string GenerateJwtToken(ApiUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
                },
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}