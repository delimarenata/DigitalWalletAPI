using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IServices
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransaction(int senderWalletId, int receiverWalletId, decimal amount);
        Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null);

    }
}

