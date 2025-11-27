using Visionet.Models;

namespace Visionet.Services
{
    public class InvoiceService
    {
        private readonly AppDbContext _db;

        public InvoiceService(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateInvoiceWithDetailsAsync(Invoice invoice, InvoiceDetail[] details)
        {
            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                await _db.Invoices.AddAsync(invoice);
                await _db.SaveChangesAsync();

                foreach (var d in details)
                {
                    d.InvoiceID = invoice.InvoiceID;
                }

                await _db.InvoiceDetails.AddRangeAsync(details);
                await _db.SaveChangesAsync();

                var calcTotal = _db.InvoiceDetails
                    .Where(x => x.InvoiceID == invoice.InvoiceID)
                    .Sum(x => (decimal?)x.Qty * x.Price) ?? 0m;

                invoice.TotalAmount = calcTotal;
                _db.Invoices.Update(invoice);
                await _db.SaveChangesAsync();

                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateInvoiceDetailsAsync(string invoiceId, InvoiceDetail[] newDetails)
        {
            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var existingDetails = _db.InvoiceDetails.Where(d => d.InvoiceID == invoiceId);
                _db.InvoiceDetails.RemoveRange(existingDetails);
                await _db.SaveChangesAsync();

                foreach (var d in newDetails)
                {
                    d.InvoiceID = invoiceId;
                }

                await _db.InvoiceDetails.AddRangeAsync(newDetails);
                await _db.SaveChangesAsync();

                var calcTotal = _db.InvoiceDetails
                    .Where(x => x.InvoiceID == invoiceId)
                    .Sum(x => (decimal?)x.Qty * x.Price) ?? 0m;

                var invoice = await _db.Invoices.FindAsync(invoiceId);
                if (invoice != null)
                {
                    invoice.TotalAmount = calcTotal;
                    _db.Invoices.Update(invoice);
                    await _db.SaveChangesAsync();
                }

                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
