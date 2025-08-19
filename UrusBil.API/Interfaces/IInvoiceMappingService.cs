using UrusBil.API.DTOs;
using UrusBil.API.Models;

namespace UrusBil.API.Interfaces
{
    public interface IInvoiceMappingService
    {
        InvoiceDto MapToDto(Invoice invoice);
        IEnumerable<InvoiceDto> MapToDto(IEnumerable<Invoice> invoices);
    }
}