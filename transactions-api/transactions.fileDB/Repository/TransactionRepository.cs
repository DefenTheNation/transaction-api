using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;
using transactions.core.Repository;

namespace transactions.fileDB.Repository
{
    public class TransactionRepository : IShopTransactionRepository
    {
        public IEnumerable<ShopTransaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Create(ShopTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Update(ShopTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Cancel(ShopTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
