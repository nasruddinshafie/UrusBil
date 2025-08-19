using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Services
{
    public class InvoiceCalculationService : IInvoiceCalculationService
    {
        public decimal CalculateSubTotal(IEnumerable<InvoiceItem> items)
        {
            return items.Sum(item => item.TotalPrice);
        }

        public decimal CalculateTaxAmount(decimal subTotal, decimal discountAmount, decimal taxRate)
        {
            var taxableAmount = subTotal - discountAmount;
            return taxableAmount > 0 ? taxableAmount * taxRate : 0;
        }

        public decimal CalculateTotalAmount(decimal subTotal, decimal discountAmount, decimal taxAmount)
        {
            return subTotal - discountAmount + taxAmount;
        }

        public void RecalculateInvoiceTotals(Invoice invoice)
        {
            invoice.SubTotal = CalculateSubTotal(invoice.Items);
            invoice.TaxAmount = CalculateTaxAmount(invoice.SubTotal, invoice.DiscountAmount, invoice.TaxRate);
            invoice.TotalAmount = CalculateTotalAmount(invoice.SubTotal, invoice.DiscountAmount, invoice.TaxAmount);
        }
    }
}