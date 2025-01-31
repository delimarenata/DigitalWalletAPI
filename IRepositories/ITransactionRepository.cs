
using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IRepositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransaction(int senderWalletId, int receiverWalletId, decimal amount);
        Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}