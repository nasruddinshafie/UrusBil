using Microsoft.EntityFrameworkCore;
using UrusBil.API.Data;
using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Customer>> SearchByNameAsync(string name)
        {
            return await _dbSet
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbSet
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}