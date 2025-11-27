namespace Visionet.Models
{

    public class InvoiceCheckResult
    {
        public string InvoiceID { get; set; }
        public decimal SystemTotal { get; set; }
        public decimal CalculatedTotal { get; set; }
        public string Status { get; set; }
    }
}
