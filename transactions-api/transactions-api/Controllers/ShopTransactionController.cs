using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using transactions.core.Model;
using transactions.core.Repository;
using transactions.core.Services;


namespace transactions.api.Controllers
{
    /// <summary>
    ///     Controller for managing transactions
    /// </summary>
    [ApiController]
    [Route("api/transactions")]   
    public class ShopTransactionController : ControllerBase
    {
        /// <summary>
        ///     Business logic layer for transactions
        /// </summary>
        private readonly ShopTransactionService _shopTransactionService;

        public ShopTransactionController(ShopTransactionService shopTransactionService)
        {
            _shopTransactionService = shopTransactionService;
        }

        /// <summary>
        ///     Return all transactions in the database
        ///     Note: Pagination would be highly recommended for live applications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ShopTransaction>> GetAll()
        {
            return Ok(_shopTransactionService.GetAllTransactions());
        }

        /// <summary>
        ///     Get an existing shop transaction
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ShopTransaction?> GetById(int id)
        {
            ShopTransaction? transaction;

            try
            {
                transaction = _shopTransactionService.GetTransaction(id);

                if (transaction == null)
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
            

            return Ok(transaction);
        }

        /// <summary>
        ///     Create a shop transaction
        /// </summary>
        /// <param name="transaction">New transaction</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] ShopTransaction transaction)
        {
            int id;

            try
            {
                id = _shopTransactionService.CreateTransaction(transaction);
            }
            catch(ArgumentException ex)
            {
                // Pass through error message from Business Logic Layer
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                // Log exception

                // Return server error
                return StatusCode(500);
            }

            return Created(id.ToString(), transaction);
        }

        /// <summary>
        ///     Update an existing transaction
        /// </summary>
        /// <param name="id">Id of the transaction</param>
        /// <param name="transaction">Transaction data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ShopTransaction transaction)
        {
            try
            {
                transaction.Id = id;
                _shopTransactionService.UpdateTransaction(transaction);
            }
            catch(ArgumentException ex)
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

        /// <summary>
        ///     Delete action - note that it does not remove a shop transaction but cancel it
        /// </summary>
        /// <param name="id">Id of the shop transaction</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _shopTransactionService.CancelTransaction(id);
                return NoContent();
            }
            catch(ArgumentException ex)
            {
                // Pass through error message from Business Logic Layer
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                // Log exception

                // Return server error
                return StatusCode(500);
            }
        }
    }
}
