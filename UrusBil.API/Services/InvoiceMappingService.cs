using UrusBil.API.DTOs;
using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Services
{
    public class InvoiceMappingService : IInvoiceMappingService
    {
        public InvoiceDto MapToDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                Customer = new CustomerDto
                {
                    Id = invoice.Customer.Id,
                    Name = invoice.Customer.Name,
                    Email = invoice.Customer.Email,
                    Phone = invoice.Customer.Phone,
                    Address = invoice.Customer.Address,
                    City = invoice.Customer.City,
                    PostalCode = invoice.Customer.PostalCode
                },
                MerchantName = invoice.MerchantName,
                MerchantAddress = invoice.MerchantAddress,
                MerchantPhone = invoice.MerchantPhone,
                MerchantEmail = invoice.MerchantEmail,
                SubTotal = invoice.SubTotal,
                TaxRate = invoice.TaxRate,
                TaxAmount = invoice.TaxAmount,
                DiscountAmount = invoice.DiscountAmount,
                TotalAmount = invoice.TotalAmount,
                Status = invoice.Status,
                Notes = invoice.Notes,
                Items = invoice.Items.Select(ii => new InvoiceItemDto
                {
                    Id = ii.Id,
                    ProductName = ii.ProductName,
                    Description = ii.Description,
                    UnitPrice = ii.UnitPrice,
                    Quantity = ii.Quantity,
                    TotalPrice = ii.TotalPrice
                }).ToList()
            };
        }

        public IEnumerable<InvoiceDto> MapToDto(IEnumerable<Invoice> invoices)
        {
            return invoices.Select(MapToDto);
        }
    }
}