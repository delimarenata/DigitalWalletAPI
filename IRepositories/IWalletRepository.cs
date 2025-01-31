using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IRepositories
{
    public interface IWalletRepository
    {
        Task<bool> CreateWallet(Wallet wallet);
        Task<Wallet> GetWalletByUserId(int userId);
        Task<Wallet> AddBalance(Wallet wallet);
        Task<Wallet> GetWalletById(int id);
        Task<TransferResult> TransferBalance(int fromWalletId, int toWalletId, decimal amount);
    }
}



