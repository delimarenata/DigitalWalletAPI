using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.IRepositories
{

    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> AddUser(User user);
        Task<User> GetUserByUsername(string username);
    }

}