using UrusBil.API.DTOs;

namespace UrusBil.API.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto?> GetInvoiceByIdAsync(int id);
        Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto createInvoiceDto);
        Task<bool> UpdateInvoiceStatusAsync(int id, string status);
        Task<IEnumerable<InvoiceDto>> GetInvoicesByCustomerAsync(int customerId);
        Task<IEnumerable<InvoiceDto>> GetOverdueInvoicesAsync();
    }
}