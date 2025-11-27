using Microsoft.AspNetCore.Mvc;
using Visionet.Services;

namespace Visionet.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceValidator _validator;

        public InvoiceController(InvoiceValidator validator)
        {
            _validator = validator;
        }

        public IActionResult Index()
        {
            var data = _validator.ValidateAllInvoices();
            return View(data);
        }

        [HttpGet]
        public IActionResult ValidateAll()
        {
            var data = _validator.ValidateAllInvoices();
            return Json(data);
        }

        [HttpGet]
        public IActionResult ValidateSingle(string id)
        {
            var result = _validator.ValidateSingleInvoice(id);
            if (result == null) return NotFound();
            return Json(result);
        }
    }
}
