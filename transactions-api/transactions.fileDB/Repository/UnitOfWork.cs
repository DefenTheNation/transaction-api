using transactions.core.Repository;
using transactions.fileDB.Repository;

namespace transactions.fileDB
{
    public class UnitOfWork : IUnitOfWork
    {
        public const string TransactionFileName = "TransactionDB.json";
        public const string InvoicesFileName = "InvoicesDB.json";

        private readonly ShopTransactionRepository _transactionRepository;
        private readonly InvoiceRepository _invoiceRepository;

        public IShopTransactionRepository TransactionRepository { get { return _transactionRepository; } }
        public IInvoiceRepository InvoiceRepository { get { return _invoiceRepository; } }

        public UnitOfWork()
        {
            _transactionRepository = new ShopTransactionRepository(TransactionFileName);
            _invoiceRepository = new InvoiceRepository(InvoicesFileName);
        }

        public void Save()
        {
            _transactionRepository.CommitToFile();
            _invoiceRepository.CommitToFile();
        }
    }
}