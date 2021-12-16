using Microsoft.AspNetCore.Mvc;
using transactions.core.Model;
using transactions.core.Repository;
using transactions.core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace transactions.api.Controllers
{
    [ApiController]
    [Route("api/transactions")]   
    public class ShopTransactionController : ControllerBase
    {
        private readonly ShopTransactionService _shopTransactionService;

        public ShopTransactionController(IUnitOfWork unitOfWork)
        {
            _shopTransactionService = new ShopTransactionService(unitOfWork);
        }


        [HttpGet]
        [Route("{id}")]
        public ShopTransaction GetShopTransactionById(int id)
        {
            var transaction = _shopTransactionService.GetTransaction(id);

            return transaction;
        }

        // POST api/<ShopTransactionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShopTransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShopTransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
