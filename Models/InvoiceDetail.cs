using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Visionet.Models;

namespace Visionet.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public string InvoiceID { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public Invoice Invoice { get; set; }
    }
}
