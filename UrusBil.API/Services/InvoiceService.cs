using UrusBil.API.DTOs;
using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInvoiceCalculationService _calculationService;
        private readonly IInvoiceMappingService _mappingService;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IInvoiceCalculationService calculationService,
            IInvoiceMappingService mappingService)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _calculationService = calculationService;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return _mappingService.MapToDto(invoices);
        }

        public async Task<InvoiceDto?> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(id);
            return invoice != null ? _mappingService.MapToDto(invoice) : null;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto createInvoiceDto)
        {
            // Validate customer exists
            if (!await _customerRepository.ExistsAsync(createInvoiceDto.CustomerId))
            {
                throw new ArgumentException("Customer not found");
            }

            // Generate invoice number
            var invoiceNumber = await _invoiceRepository.GenerateInvoiceNumberAsync();

            // Create invoice
            var invoice = new Invoice
            {
                InvoiceNumber = invoiceNumber,
                CustomerId = createInvoiceDto.CustomerId,
                DueDate = createInvoiceDto.DueDate,
                MerchantName = createInvoiceDto.MerchantName,
                MerchantAddress = createInvoiceDto.MerchantAddress,
                MerchantPhone = createInvoiceDto.MerchantPhone,
                MerchantEmail = createInvoiceDto.MerchantEmail,
                DiscountAmount = createInvoiceDto.DiscountAmount,
                Notes = createInvoiceDto.Notes
            };

            // Add invoice items
            foreach (var itemDto in createInvoiceDto.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {itemDto.ProductId} not found");
                }

                var unitPrice = itemDto.UnitPrice ?? product.Price;
                var totalPrice = unitPrice * itemDto.Quantity;

                var invoiceItem = new InvoiceItem
                {
                    ProductId = itemDto.ProductId,
                    ProductName = product.Name,
                    Description = product.Description,
                    UnitPrice = unitPrice,
                    Quantity = itemDto.Quantity,
                    TotalPrice = totalPrice
                };

                invoice.Items.Add(invoiceItem);
            }

            // Calculate totals
            _calculationService.RecalculateInvoiceTotals(invoice);

            // Save invoice
            var createdInvoice = await _invoiceRepository.CreateAsync(invoice);
            var invoiceWithDetails = await _invoiceRepository.GetInvoiceWithDetailsAsync(createdInvoice.Id);

            return _mappingService.MapToDto(invoiceWithDetails!);
        }

        public async Task<bool> UpdateInvoiceStatusAsync(int id, string status)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                return false;

            invoice.Status = status;
            if (status == "Paid" && invoice.PaidDate == null)
            {
                invoice.PaidDate = DateTime.UtcNow;
            }

            await _invoiceRepository.UpdateAsync(invoice);
            return true;
        }

        public async Task<IEnumerable<InvoiceDto>> GetInvoicesByCustomerAsync(int customerId)
        {
            var invoices = await _invoiceRepository.GetInvoicesByCustomerAsync(customerId);
            return _mappingService.MapToDto(invoices);
        }

        public async Task<IEnumerable<InvoiceDto>> GetOverdueInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetOverdueInvoicesAsync();
            return _mappingService.MapToDto(invoices);
        }
    }
}