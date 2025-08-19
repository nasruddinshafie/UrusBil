using UrusBil.API.Models;

namespace UrusBil.API.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<Invoice?> GetInvoiceWithDetailsAsync(int id);
        Task<IEnumerable<Invoice>> GetInvoicesByCustomerAsync(int customerId);
        Task<IEnumerable<Invoice>> GetInvoicesByStatusAsync(string status);
        Task<IEnumerable<Invoice>> GetOverdueInvoicesAsync();
        Task<string> GenerateInvoiceNumberAsync();
    }
}
