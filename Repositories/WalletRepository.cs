
using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Repositories

{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;


        public WalletRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Wallet> AddBalance(Wallet wallet)
        {

            wallet.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return wallet;
        }

        public async Task<bool> CreateWallet(Wallet wallet)
        {

            _context.Wallets.Add(wallet);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<Wallet> GetWalletByUserId(int userId)
        {
            try
            {
                var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
                return wallet;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error accessing the database.", ex);
            }
        }

        public async Task<Wallet> GetWalletById(int id)
        {
            try
            {
                var wallet = await _context.Wallets.FindAsync(id);
                return wallet;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error accessing the database.", ex);
            }
        }


        public async Task<TransferResult> TransferBalance(int fromWalletId, int toWalletId, decimal amount)
        {
            try
            {
                var fromWallet = await _context.Wallets.FindAsync(fromWalletId);
                var toWallet = await _context.Wallets.FindAsync(toWalletId);

                if (fromWallet == null || toWallet == null)
                {
                    return new TransferResult(false, "Uma ou ambas as carteiras são inválidas.");
                }

                if (fromWallet.Balance < amount)
                {
                    return new TransferResult(false, "Saldo insuficiente na carteira de origem.");
                }

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        fromWallet.Balance -= amount;
                        toWallet.Balance += amount;

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return new TransferResult(true, "Transferência realizada com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return new TransferResult(false, "Erro ao realizar a transferência: " + ex.Message);
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                return new TransferResult(false, "Erro ao acessar o banco de dados: " + ex.Message);
            }
        }


    }
}