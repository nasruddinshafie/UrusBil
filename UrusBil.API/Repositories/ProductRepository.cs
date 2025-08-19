using Microsoft.EntityFrameworkCore;
using UrusBil.API.Data;
using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            return await _dbSet
                .Where(p => p.Name.Contains(name) && p.IsActive)
                .ToListAsync();
        }

        public async Task<bool> DeactivateProductAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
                return false;

            product.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}