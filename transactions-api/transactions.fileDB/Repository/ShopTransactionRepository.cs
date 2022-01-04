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
    public class ShopTransactionRepository : IShopTransactionRepository
    {
        protected readonly string _databaseFileName;
        protected List<ShopTransaction> _shopTransactions;

        public ShopTransactionRepository(string filename)
        {
            _databaseFileName = filename;
            _shopTransactions = new List<ShopTransaction>();

            LoadDataFromFile();
        }

        protected void LoadDataFromFile()
        {
            if(File.Exists(_databaseFileName))
            {
                string contents = File.ReadAllText(_databaseFileName);
                _shopTransactions = JsonConvert.DeserializeObject<List<ShopTransaction>>(contents) ?? new List<ShopTransaction>();
            }
            else
            {
                _shopTransactions = new List<ShopTransaction>();
            }
        }

        public void CommitToFile()
        {
            string contents = JsonConvert.SerializeObject(_shopTransactions);

            File.WriteAllText(_databaseFileName, contents);
        }

        public IEnumerable<ShopTransaction> GetAll()
        {
            return _shopTransactions;
        }

        public int Create(ShopTransaction transaction)
        {
            transaction.Id = _shopTransactions.Count;
            _shopTransactions.Add(transaction);

            return transaction.Id;
        }

        public void Update(ShopTransaction transaction)
        {
            _shopTransactions[transaction.Id] = transaction;
        }

        public IEnumerable<ShopTransaction> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            foreach(var transaction in _shopTransactions)
            {
                if(transaction.TransactionDateTime >= startDate && transaction.TransactionDateTime <= endDate)
                {
                    yield return transaction;
                }
            }
        }

        public ShopTransaction? Get(int id)
        {
            if (id >= _shopTransactions.Count)
            {
                return null;
            }                
            else
            {
                return _shopTransactions[id];
            }
        }       

        /// <summary>
        ///     Get transactions by Invoice Id. This should only be used by the repository project as it is an implementation detail.
        ///     ORMs will generally manage these relationships for the programmer.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        internal IEnumerable<ShopTransaction> GetByInvoiceId(int invoiceId)
        {
            foreach(var transaction in _shopTransactions)
            {
                if(transaction.InvoiceId == invoiceId)
                {
                    yield return transaction;
                }
            }
        }
    }
}
