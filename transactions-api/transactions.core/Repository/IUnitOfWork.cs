using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transactions.core.Repository
{
    public interface IUnitOfWork
    {
        IShopTransactionRepository TransactionRepository { get; set; }
        IInvoiceRepository InvoiceRepository { get; set; }

        /// <summary>
        ///     Commit all data transaction(s)
        /// </summary>
        void Save();
    }
}
