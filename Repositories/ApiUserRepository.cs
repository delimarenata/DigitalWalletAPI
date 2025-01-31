using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Repositories
{

    public class ApiUserRepository : IApiUserRepository
    {
        private readonly DataContext _context;

        public ApiUserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiUser> GetByEmailAsync(string email)
        {
            return await _context.ApiUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(ApiUser apiUser)
        {
            await _context.ApiUsers.AddAsync(apiUser);
            await _context.SaveChangesAsync();
        }
    }
}