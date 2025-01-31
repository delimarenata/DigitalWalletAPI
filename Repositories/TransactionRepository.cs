using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Repositories
{

    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateTransaction(int senderWalletId, int receiverWalletId, decimal amount)
        {
            var senderWallet = await _context.Wallets.FindAsync(senderWalletId);
            var receiverWallet = await _context.Wallets.FindAsync(receiverWalletId);

            if (senderWallet == null || receiverWallet == null)
                throw new InvalidOperationException("Uma ou ambas as carteiras não foram encontradas.");

            if (senderWallet.Balance < amount)
                throw new InvalidOperationException("Saldo insuficiente para realizar a transação.");

            var transaction = new Transaction
            {
                SenderWallet = senderWallet,
                ReceiverWallet = receiverWallet,
                SenderWalletId = senderWalletId,
                ReceiverWalletId = receiverWalletId,
                Amount = amount,
                Status = "Completed",
                CreatedAt = DateTime.UtcNow
            };

            senderWallet.Balance -= amount;
            receiverWallet.Balance += amount;
            senderWallet.UpdatedAt = DateTime.UtcNow;
            receiverWallet.UpdatedAt = DateTime.UtcNow;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (startDate.HasValue && startDate.Value.Kind == DateTimeKind.Unspecified)
            {
                startDate = DateTime.SpecifyKind(startDate.Value, DateTimeKind.Utc);
            }

            if (endDate.HasValue && endDate.Value.Kind == DateTimeKind.Unspecified)
            {
                endDate = DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc);
            }

            if (startDate.HasValue)
            {
                startDate = startDate.Value.Date.ToUniversalTime();
            }

            if (endDate.HasValue)
            {
                endDate = endDate.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();
            }

            var transactions = await _context.Transactions
                .Where(t => (t.SenderWallet.UserId == userId || t.ReceiverWallet.UserId == userId) &&
                            (startDate == null || t.CreatedAt >= startDate) &&
                            (endDate == null || t.CreatedAt <= endDate))
                .ToListAsync();

            return transactions;
        }


    }
}