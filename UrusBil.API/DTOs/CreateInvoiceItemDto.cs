namespace UrusBil.API.DTOs
{
    public class CreateInvoiceItemDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? UnitPrice { get; set; } // Optional override
    }
}
