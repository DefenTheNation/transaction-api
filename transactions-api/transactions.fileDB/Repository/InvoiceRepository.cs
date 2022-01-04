using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;
using transactions.core.Repository;

namespace transactions.fileDB.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        protected readonly string _databaseFileName;
        protected List<Invoice> _invoices;
        protected ShopTransactionRepository _shopTransactionRepository;

        public InvoiceRepository(string invoiceFileName, string transactionFileName)
        {
            _databaseFileName = invoiceFileName;
            _invoices = new List<Invoice>();
            _shopTransactionRepository = new ShopTransactionRepository(transactionFileName);

            LoadDataFromFile();
        }

        protected void LoadDataFromFile()
        {
            if (File.Exists(_databaseFileName))
            {
                string contents = File.ReadAllText(_databaseFileName);
                _invoices = JsonConvert.DeserializeObject<List<Invoice>>(contents) ?? new List<Invoice>();
            }
            else
            {
                _invoices = new();
            }
        }

        public void CommitToFile()
        {
            string contents = JsonConvert.SerializeObject(_invoices);

            File.WriteAllText(_databaseFileName, contents);
        }

        public IEnumerable<Invoice> GetAll()
        {
            foreach(var invoice in _invoices)
            {
                invoice.Transactions = _shopTransactionRepository.GetByInvoiceId(invoice.Id);
            }

            return _invoices;
        }

        public Invoice? Get(int id)
        {
            if (id >= _invoices.Count)
            {
                return null;
            }
            else
            {
                Invoice invoice = _invoices[id];
                invoice.Transactions = _shopTransactionRepository.GetByInvoiceId(id);

                return invoice;
            }
        }
        public int Create(Invoice invoice)
        {
            invoice.Id = _invoices.Count;
            _invoices.Add(invoice);

            SaveTransactionsForInvoice(invoice);

            return invoice.Id;
        }

        public void Update(Invoice invoice)
        {
            _invoices[invoice.Id] = invoice;

            SaveTransactionsForInvoice(invoice);
        }

        protected void SaveTransactionsForInvoice(Invoice invoice)
        {
            if (invoice.Transactions == null) return;

            foreach (var transaction in invoice.Transactions)
            {
                transaction.InvoiceId = invoice.Id;

                if (_shopTransactionRepository.Get(transaction.Id) != null)
                {
                    _shopTransactionRepository.Update(transaction);
                }
                else
                {
                    _shopTransactionRepository.Create(transaction);
                }

            }
        }
    }
}
