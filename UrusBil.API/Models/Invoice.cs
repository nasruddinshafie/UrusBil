namespace UrusBil.API.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }

        // Customer information
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        // Merchant information
        public string MerchantName { get; set; } = string.Empty;
        public string MerchantAddress { get; set; } = string.Empty;
        public string MerchantPhone { get; set; } = string.Empty;
        public string MerchantEmail { get; set; } = string.Empty;

        // Invoice details
        public decimal SubTotal { get; set; }
        public decimal TaxRate { get; set; } = 0.06m; // 6% SST
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Draft"; // Draft, Sent, Paid, Overdue, Cancelled
        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? PaidDate { get; set; }

        // Navigation properties
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}
