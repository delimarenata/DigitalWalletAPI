using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IRepositories
{
    public interface IApiUserRepository
    {
        Task<ApiUser> GetByEmailAsync(string email);
        Task AddAsync(ApiUser apiUser);
    }
}