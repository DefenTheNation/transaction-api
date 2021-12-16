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
        public int Create(ShopTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Invoice Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ShopTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
