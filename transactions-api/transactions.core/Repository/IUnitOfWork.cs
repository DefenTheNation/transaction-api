using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transactions.core.Repository
{
    public interface IUnitOfWork
    {
        IShopTransactionRepository TransactionRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }

        /// <summary>
        ///     Commit all data transaction(s)
        /// </summary>
        void Save();
    }
}
