using Visionet.Models;

namespace Visionet.Helper
{
    public static class SampleData
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Invoices.Any()) return;

            var inv1 = new Invoice { InvoiceID = "INV001", TotalAmount = 250000m };
            db.Invoices.Add(inv1);
            db.InvoiceDetails.Add(new InvoiceDetail { InvoiceID = "INV001", Qty = 2, Price = 125000m });

            var inv2 = new Invoice { InvoiceID = "INV002", TotalAmount = 90000m };
            db.Invoices.Add(inv2);
            db.InvoiceDetails.Add(new InvoiceDetail { InvoiceID = "INV002", Qty = 1, Price = 90000m });

            var inv3 = new Invoice { InvoiceID = "INV003", TotalAmount = 180000m };
            db.Invoices.Add(inv3);
            db.InvoiceDetails.Add(new InvoiceDetail { InvoiceID = "INV003", Qty = 2, Price = 90000m });

            var inv4 = new Invoice { InvoiceID = "INV004", TotalAmount = 200000m };
            db.Invoices.Add(inv4);
            db.InvoiceDetails.Add(new InvoiceDetail { InvoiceID = "INV004", Qty = 2, Price = 110000m });

            db.SaveChanges();
        }
    }
}
