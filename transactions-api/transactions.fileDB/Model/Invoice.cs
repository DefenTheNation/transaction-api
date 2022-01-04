using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transactions.fileDB.Model
{
    internal class Invoice
    {
        public Invoice()
        {

        }

        public Invoice(core.Model.Invoice modelInvoice)
        {
            Id = modelInvoice.Id;
            Name = modelInvoice.Name;
            Description = modelInvoice.Description;

            List<ShopTransaction> transactions = new();

            if(modelInvoice.Transactions != null)
            {
                foreach (var transaction in modelInvoice.Transactions)
                {
                    transactions.Add(new ShopTransaction(transaction));
                }
            }
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<ShopTransaction>? Transactions { get; set; }

        public core.Model.Invoice ToModel()
        {
            List<core.Model.ShopTransaction> modelTransactions = new();

            if(Transactions != null)
            {
                foreach (var transaction in Transactions)
                {
                    modelTransactions.Add(transaction.ToModel());
                }
            }         

            return new core.Model.Invoice()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Transactions = modelTransactions
            };
        }
    }
}
