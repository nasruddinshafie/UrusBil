namespace UrusBil.API.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public CustomerDto Customer { get; set; } = null!;
        public string MerchantName { get; set; } = string.Empty;
        public string MerchantAddress { get; set; } = string.Empty;
        public string MerchantPhone { get; set; } = string.Empty;
        public string MerchantEmail { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}
