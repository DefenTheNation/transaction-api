using transactions.core.Repository;
using transactions.fileDB.Repository;

namespace transactions.fileDB
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopTransactionRepository _transactionRepository;
        private readonly InvoiceRepository _invoiceRepository;

        public IShopTransactionRepository TransactionRepository { get { return _transactionRepository; } }
        public IInvoiceRepository InvoiceRepository { get { return _invoiceRepository; } }

        public UnitOfWork(string transactionDBFileName, string invoiceDBFileName)
        {
            SetupFilesystem(transactionDBFileName);
            SetupFilesystem(invoiceDBFileName);

            _transactionRepository = new ShopTransactionRepository(transactionDBFileName);
            _invoiceRepository = new InvoiceRepository(invoiceDBFileName, transactionDBFileName);
        }

        /// <summary>
        ///     Creates the folder for the specified file name if it does not exist
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="Exception"></exception>
        protected static void SetupFilesystem(string fileName)
        {
            var path = Path.GetDirectoryName(fileName);

            if (path == null)
                throw new Exception("Unable to setup file system for file database!");

            // If the directory already exists, this function does nothing
            // CreateDirectory already performs the "Does the folder exist?" check
            Directory.CreateDirectory(path);
        }

        public void Save()
        {
            _transactionRepository.CommitToFile();
            _invoiceRepository.CommitToFile();
        }
    }
}