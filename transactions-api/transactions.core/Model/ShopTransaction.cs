using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transactions.core.Model
{
    public class ShopTransaction
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public ShopTransactionStatusType Status { get; set; }
    }
}
