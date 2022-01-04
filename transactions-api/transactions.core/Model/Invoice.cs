using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transactions.core.Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionStartDate { get; set; }
        public DateTime TransactionEndDate { get; set; }
        public IEnumerable<ShopTransaction>? Transactions { get; set; }
    }
}
