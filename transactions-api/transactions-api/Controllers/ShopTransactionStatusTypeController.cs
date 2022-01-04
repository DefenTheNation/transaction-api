using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using transactions.core.Model;

namespace transactions.api.Controllers
{
    [Route("api/transactionTypes")]
    [ApiController]
    public class ShopTransactionStatusTypeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //var values = Enum.GetValues<ShopTransactionStatusType>();
            var names = Enum.GetNames<ShopTransactionStatusType>();

            return Ok(names);
        }
    }
}
