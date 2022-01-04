using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;
using transactions.core.Repository;

namespace transactions.core.Services
{
    /// <summary>
    ///     Business logic layer for invoices
    /// </summary>
    public class InvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Get an enumerable of all invoices
        /// </summary>
        /// <returns>Enumerable of invoices</returns>
        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _unitOfWork.InvoiceRepository.GetAll();
        }

        /// <summary>
        ///     Get invoice from the repository
        /// </summary>
        /// <param name="invoiceId">Id of the invoice</param>
        /// <returns>Invoice if found, null if not found</returns>
        public Invoice? GetInvoice(int invoiceId)
        {
            return _unitOfWork.InvoiceRepository.Get(invoiceId);
        }

        /// <summary>
        ///     Create a new invoice and update the unbilled transactions to have status billed. 
        ///     Transactions that are already billed, paid, or cancelled will not be added to the invoice.
        /// </summary>
        /// <param name="invoice">New invoice</param>
        /// <param name="transactionStartDate">Start date for the transactions to be posted on the invoice</param>
        /// <param name="transactionEndDate">End date for the transactions to be posted on the invoice</param>
        public int CreateInvoice(Invoice invoice)
        {
            int id;
            List<ShopTransaction> transactionsInDateRange = _unitOfWork.TransactionRepository.GetByDateRange(invoice.TransactionStartDate, invoice.TransactionEndDate).ToList();
            List<ShopTransaction> unbilledTransactions = new();
            
            foreach (var transaction in transactionsInDateRange)
            {
                if(transaction.Status == ShopTransactionStatusType.Unbilled)
                {
                    unbilledTransactions.Add(transaction);
                    transaction.Status = ShopTransactionStatusType.Billed;

                    _unitOfWork.TransactionRepository.Update(transaction);
                }
            }

            invoice.Transactions = unbilledTransactions;
            id = _unitOfWork.InvoiceRepository.Create(invoice);
            _unitOfWork.Save();

            return id;
        }

        /// <summary>
        ///     Update an existing invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <exception cref="ArgumentException">Invoice has no transactions</exception>
        public void UpdateInvoice(Invoice invoice)
        {
            if(invoice.Transactions == null || !invoice.Transactions.Any())
            {
                throw new ArgumentException("Cannot remove all transactions from invoice!");
            }

            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Mark an invoice and its transactions as paid. If a transaction is cancelled, then do not mark as paid.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>Paid invoice</returns>
        /// <exception cref="ArgumentException">Invoice was not found or has no transactions</exception>
        public Invoice PayInvoice(int invoiceId)
        {
            Invoice? invoice = _unitOfWork.InvoiceRepository.Get(invoiceId);

            if (invoice == null)
            {
                throw new ArgumentException("Invoice with id " + invoiceId + " not found!");
            }
            else if(invoice.Transactions == null || !invoice.Transactions.Any())
            {
                throw new ArgumentException("Invoice with id " + invoiceId + " has no transactions! Payment could not be made.");
            }

            // Ignore cancelled transactions
            foreach(var transaction in invoice.Transactions)
            {
                if(transaction.Status != ShopTransactionStatusType.Cancelled)
                {
                    transaction.Status = ShopTransactionStatusType.Paid;
                }
            }

            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.Save();

            return invoice;
        }
    }
}
