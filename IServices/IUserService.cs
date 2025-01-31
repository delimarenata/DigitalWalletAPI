using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IServices
{
    public interface IUserService
    {
        Task<User> AddUser(string username, string email, string password);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
    }
}
