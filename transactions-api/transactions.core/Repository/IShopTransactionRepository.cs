using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;

namespace transactions.core.Repository
{
    public interface IShopTransactionRepository
    {
        /// <summary>
        ///     Get all transactions
        /// </summary>
        /// <returns></returns>
        IEnumerable<ShopTransaction> GetAll();

        /// <summary>
        ///     Get a list of transactions by date range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IEnumerable<ShopTransaction> GetByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        ///     Get single transaction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ShopTransaction Get(int id);

        /// <summary>
        ///     Create a transaction and get an id in return
        /// </summary>
        /// <param name="transaction">Incoming transaction</param>
        /// <returns></returns>
        int Create(ShopTransaction transaction);

        /// <summary>
        ///     Apply updates to a transaction
        /// </summary>
        /// <param name="transaction"></param>
        void Update(ShopTransaction transaction);
    }
}
