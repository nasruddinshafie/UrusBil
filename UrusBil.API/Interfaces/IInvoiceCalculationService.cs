using UrusBil.API.Models;

namespace UrusBil.API.Interfaces
{
    public interface IInvoiceCalculationService
    {
        decimal CalculateSubTotal(IEnumerable<InvoiceItem> items);
        decimal CalculateTaxAmount(decimal subTotal, decimal discountAmount, decimal taxRate);
        decimal CalculateTotalAmount(decimal subTotal, decimal discountAmount, decimal taxAmount);
        void RecalculateInvoiceTotals(Invoice invoice);
    }
}