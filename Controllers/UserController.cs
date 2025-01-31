using Microsoft.AspNetCore.Mvc;
using DigitalWalletAPI.IServices;
using DigitalWalletAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace DigitalWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null || string.IsNullOrEmpty(createUserDto.Email))
            {
                return BadRequest("Dados inválidos.");
            }

            var existingUser = await _userService.GetUserByEmail(createUserDto.Email);
            if (existingUser != null)
            {
                return BadRequest("E-mail já cadastrado.");
            }

            if (string.IsNullOrEmpty(createUserDto.Username) ||
                string.IsNullOrEmpty(createUserDto.Email) ||
                string.IsNullOrEmpty(createUserDto.Password))
            {
                return BadRequest("Campos obrigatórios não informados.");
            }

            var user = await _userService.AddUser(createUserDto.Username, createUserDto.Email, createUserDto.Password);

            if (user == null)
            {
                return BadRequest("Falha ao criar usuário.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro inexperado!");
            }
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return NotFound("E-mail não encontrado.");
            }

            return Ok(user);
        }
    }
}
