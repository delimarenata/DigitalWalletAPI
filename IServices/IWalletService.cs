using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IServices
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(int userId);
        Task<Wallet> GetWalletByUserId(int userId);
        Task<Wallet> AddBalance(int walletId, decimal amount);
        Task<TransferResult> TransferBalanceBetweenUsers(int fromUserId, int toUserId, decimal amount);
    }
}
