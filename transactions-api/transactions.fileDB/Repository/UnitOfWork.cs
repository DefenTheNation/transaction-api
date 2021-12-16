using transactions.core.Repository;
using transactions.fileDB.Repository;

namespace transactions.fileDB
{
    public class UnitOfWork : IUnitOfWork
    {
        public IShopTransactionRepository TransactionRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }

        public UnitOfWork()
        {
            TransactionRepository = new TransactionRepository();
            InvoiceRepository = new InvoiceRepository();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}