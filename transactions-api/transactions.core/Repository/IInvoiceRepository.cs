using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;

namespace transactions.core.Repository
{
    public interface IInvoiceRepository
    {
        /// <summary>
        ///     Get all invoices
        /// </summary>
        /// <returns></returns>
        IEnumerable<Invoice> GetAll();

        /// <summary>
        ///     Get an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Invoice Get(int id);

        /// <summary>
        ///     Create a new invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        int Create(Invoice invoice);

        /// <summary>
        ///     Apply updates to an existing invoice
        /// </summary>
        /// <param name="invoice"></param>
        void Update(Invoice invoice);
    }
}
