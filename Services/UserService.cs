using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.IServices;
using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public UserService(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<User> AddUser(string username, string email, string password)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(email);
                if (existingUser != null)
                {
                    throw new Exception("E-mail já cadastrado.");
                }

                var user = new User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
                };

                await _userRepository.AddUser(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário.", ex);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentException("O e-mail do usuário não pode ser vazio!", nameof(email));
            }

            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao buscar o usuário por e-mail.", ex);
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("O id do usuário deve ser maior que zero!", nameof(userId));
            }

            try
            {
                var user = await _userRepository.GetUserById(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao buscar o usuário por ID.", ex);
            }
        }


        public async Task<User> GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("O nome de usuário não pode ser nulo, vazio ou conter apenas espaços.", nameof(username));
            }

            try
            {
                var user = await _userRepository.GetUserByUsername(username);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao buscar o usuário por nome de usuário.", ex);
            }
        }
    }
}

