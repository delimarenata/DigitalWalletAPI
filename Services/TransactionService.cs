using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.IServices;
using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(DataContext context, ITransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }


        public async Task<Transaction> CreateTransaction(int senderWalletId, int receiverWalletId, decimal amount)
        {
            return await _transactionRepository.CreateTransaction(senderWalletId, receiverWalletId, amount);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserId(userId, startDate, endDate);

            return transactions;
        }
    }
}
