using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestDTO request)
        {
            try
            {
                var transaction = await _transactionService.CreateTransaction(request.SenderWalletId, request.ReceiverWalletId, request.Amount);
                return Ok(transaction);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByUserId(userId, startDate, endDate);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }
    }
}
