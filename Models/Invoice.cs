

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Visionet.Models
{
    public class Invoice
    {
        public string InvoiceID { get; set; }  // PK
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<InvoiceDetail> Details { get; set; }
    }

}
