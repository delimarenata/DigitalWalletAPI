using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(User user)
        {
            _context.Users.Add(user);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users
                    .OrderByDescending(u => u.Id)
                    .Take(10)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os últimos 10 usuários.", ex);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error accessing the database.", ex);
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error accessing the database.", ex);
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
                var user = await _context.Users.FirstOrDefaultAsync(u =>
                                u.Username != null && u.Username.ToUpper() == username.ToUpper());
                return user;
            }
            catch (Exception ex)
            {
                // Relançar a exceção com detalhes preservados
                throw new Exception("Falha ao buscar o usuário por nome de usuário.", ex);
            }
        }
    }
}