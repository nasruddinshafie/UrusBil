using Microsoft.EntityFrameworkCore;
using UrusBil.API.Data;
using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Invoice?> GetInvoiceWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .ThenInclude(ii => ii.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByCustomerAsync(int customerId)
        {
            return await _dbSet
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .Where(i => i.CustomerId == customerId)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByStatusAsync(string status)
        {
            return await _dbSet
                .Include(i => i.Customer)
                .Where(i => i.Status == status)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetOverdueInvoicesAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet
                .Include(i => i.Customer)
                .Where(i => i.DueDate < today && i.Status != "Paid" && i.Status != "Cancelled")
                .ToListAsync();
        }

        public async Task<string> GenerateInvoiceNumberAsync()
        {
            var today = DateTime.Now;
            var prefix = $"INV{today:yyyyMM}";

            var lastInvoice = await _dbSet
                .Where(i => i.InvoiceNumber.StartsWith(prefix))
                .OrderByDescending(i => i.InvoiceNumber)
                .FirstOrDefaultAsync();

            if (lastInvoice == null)
            {
                return $"{prefix}001";
            }

            var lastNumber = int.Parse(lastInvoice.InvoiceNumber.Substring(prefix.Length));
            return $"{prefix}{(lastNumber + 1):D3}";
        }

        public override async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _dbSet
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .ThenInclude(ii => ii.Product)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
        }
    }
}