using Visionet.Models;

namespace Visionet.Services
{
    public class InvoiceValidator
    {
        private readonly AppDbContext _db;

        public InvoiceValidator(AppDbContext db)
        {
            _db = db;
        }

        public List<InvoiceCheckResult> ValidateAllInvoices()
        {
            // Gunakan GroupJoin + Sum agar single query
            var results = _db.Invoices
                .Select(inv => new InvoiceCheckResult
                {
                    InvoiceID = inv.InvoiceID,
                    SystemTotal = inv.TotalAmount,
                    CalculatedTotal = _db.InvoiceDetails
                        .Where(d => d.InvoiceID == inv.InvoiceID)
                        .Sum(d => (decimal?)d.Qty * d.Price) ?? 0m,
                    Status = ""
                })
                .AsEnumerable()
                .Select(x =>
                {
                    x.Status = x.SystemTotal == x.CalculatedTotal ? "Valid" : "Mismatch";
                    return x;
                })
                .ToList();

            return results;
        }

        public InvoiceCheckResult ValidateSingleInvoice(string invoiceId)
        {
            var inv = _db.Invoices.Find(invoiceId);
            if (inv == null) return null;

            var calc = _db.InvoiceDetails
                .Where(d => d.InvoiceID == invoiceId)
                .Sum(d => (decimal?)d.Qty * d.Price) ?? 0m;

            return new InvoiceCheckResult
            {
                InvoiceID = inv.InvoiceID,
                SystemTotal = inv.TotalAmount,
                CalculatedTotal = calc,
                Status = inv.TotalAmount == calc ? "Valid" : "Mismatch"
            };
        }
    }
}
