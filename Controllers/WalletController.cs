using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.IServices;
using DigitalWalletAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigitalWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("create-wallet/{userId}")]
        public async Task<IActionResult> CreateWallet(int userId)
        {
            try
            {
                var wallet = await _walletService.CreateWallet(userId);
                return Ok(wallet);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-balance/{walletId}")]
        [SwaggerOperation(Summary = "Adiciona um valor ao saldo da carteira",
                         Description = "Ao fornecer o valor, use ponto (.) como separador decimal. Exemplo: 1.99 em vez de 1,99.")]
        public async Task<IActionResult> AddBalance(int walletId, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("O valor a ser adicionado deve ser maior que zero.");
            }

            if (amount.ToString().Contains(","))
            {
                return BadRequest(new { error = "Formato inválido. Use ponto (.) em vez de vírgula (,). Exemplo: 1.99" });
            }

            var wallet = await _walletService.AddBalance(walletId, amount);

            if (wallet == null)
            {
                return NotFound("Carteira não encontrada.");
            }

            return Ok(new { Balance = wallet.Balance });
        }

        [HttpGet("get-wallet/{userId}")]
        public async Task<IActionResult> GetWalletByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { message = "O ID do usuário deve ser maior que zero!" });
            }

            try
            {
                var wallet = await _walletService.GetWalletByUserId(userId);

                if (wallet == null)
                {
                    return NotFound(new { message = "Carteira não encontrada para esse usuário." });
                }

                // Converte o modelo para DTO antes de retornar
                var walletDTO = new WalletDTO
                {
                    Id = wallet.Id,
                    UserId = wallet.UserId,
                    Balance = wallet.Balance,
                    CreatedAt = wallet.CreatedAt,
                    UpdatedAt = wallet.UpdatedAt
                };

                return Ok(walletDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar a carteira.", error = ex.Message });
            }
        }


        [HttpGet("get-balance/{userId}")]
        public async Task<IActionResult> GetBalanceByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { message = "O ID do usuário deve ser maior que zero!" });
            }

            try
            {
                var wallet = await _walletService.GetWalletByUserId(userId);

                if (wallet == null)
                {
                    return NotFound(new { message = "Carteira não encontrada para esse usuário." });
                }

                var balance = wallet.Balance;

                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar o saldo.", error = ex.Message });
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferBalance(int fromUserId, int toUserId, decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest(new TransferResult(false, "O valor da transferência deve ser positivo."));
            }

            var transferResult = await _walletService.TransferBalanceBetweenUsers(fromUserId, toUserId, amount);

            if (transferResult?.IsSuccess == true)
            {
                return Ok(transferResult);
            }

            return BadRequest(transferResult);
        }
    }
}


