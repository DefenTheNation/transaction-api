using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using transactions.core.Model;
using transactions.core.Repository;
using transactions.core.Services;

namespace transactions.api.Controllers
{
    [ApiController]
    [Route("api/invoices")] 
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Invoice>> GetAll()
        {
            return Ok(_invoiceService.GetAllInvoices());
        }

        [HttpGet("{Id}")]
        public ActionResult<Invoice> Get(int id)
        {
            Invoice? invoice;

            try
            {
                invoice = _invoiceService.GetInvoice(id);

                if(invoice == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                // Log exception

                // Return server error
                return StatusCode(500);
            }

            return Ok(invoice);
        }

        [HttpPost]
        public ActionResult<Invoice> Post([FromBody] Invoice invoice)
        {
            int id;

            try
            {
                id = _invoiceService.CreateInvoice(invoice);
            }
            catch (ArgumentException ex)
            {
                // Pass through error message from Business Logic Layer
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Log exception

                // Return server error
                return StatusCode(500);
            }

            return Created(id.ToString(), invoice);
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int id, [FromBody] Invoice invoice)
        {
            try
            {
                invoice.Id = id;
                _invoiceService.UpdateInvoice(invoice);
            }
            catch (ArgumentException ex)
            {
                // Pass through error message from Business Logic Layer
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Log exception

                // Return server error
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
