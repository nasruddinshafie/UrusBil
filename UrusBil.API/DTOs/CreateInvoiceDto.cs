namespace UrusBil.API.DTOs
{
    public class CreateInvoiceDto
    {
        public int CustomerId { get; set; }
        public DateTime DueDate { get; set; }
        public string MerchantName { get; set; } = string.Empty;
        public string MerchantAddress { get; set; } = string.Empty;
        public string MerchantPhone { get; set; } = string.Empty;
        public string MerchantEmail { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }
}
