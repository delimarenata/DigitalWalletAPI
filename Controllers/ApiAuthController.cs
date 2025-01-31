using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IApiUserService _apiUserService;

        public ApiAuthController(IApiUserService apiUserService)
        {
            _apiUserService = apiUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ApiUserDto userDto)
        {
            await _apiUserService.RegisterAsync(userDto);
            return Ok(new { Message = "Usuário da API registrado com sucesso!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ApiUserLoginDto loginDto)
        {
            var token = await _apiUserService.AuthenticateAsync(loginDto);
            return Ok(new { Token = token });
        }
    }
}
