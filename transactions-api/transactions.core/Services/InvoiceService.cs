using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;
using transactions.core.Repository;

namespace transactions.core.Services
{
    public class InvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Invoice GetInvoice(int invoiceId)
        {
            return _unitOfWork.InvoiceRepository.Get(invoiceId);
        }

        public void CreateInvoice(Invoice invoice, DateTime transactionStartDate, DateTime transactionEndDate)
        {
            invoice.Transactions = _unitOfWork.TransactionRepository.GetByDateRange(transactionStartDate, transactionEndDate);

            _unitOfWork.InvoiceRepository.Create(invoice);
            _unitOfWork.Save();
        }

        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice));
            }
            else if(invoice.Transactions == null || !invoice.Transactions.Any())
            {
                throw new ArgumentException("Cannot remove all transactions from invoice!");
            }

            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.Save();
        }
    }
}
