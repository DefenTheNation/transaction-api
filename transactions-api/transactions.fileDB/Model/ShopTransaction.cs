using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;

namespace transactions.fileDB.Model
{
    /// <summary>
    ///     Repository ShopTransaction. This should only be referenced from the repository project
    /// </summary>
    internal class ShopTransaction
    {
        public ShopTransaction()
        {

        }

        //
        public ShopTransaction(core.Model.ShopTransaction modelTransaction)
        {
            Id = modelTransaction.Id;
            InvoiceId = modelTransaction.InvoiceId;
            Description = modelTransaction.Description;
            TransactionDateTime = modelTransaction.TransactionDateTime;
            Amount = modelTransaction.Amount;
            Status = (ShopTransactionStatusType)modelTransaction.Status;
        }

        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public ShopTransactionStatusType Status { get; set; }

        /// <summary>
        ///     Map the repository model to the core model
        /// </summary>
        /// <returns></returns>
        public core.Model.ShopTransaction ToModel()
        {
            return new core.Model.ShopTransaction()
            {
                Id = Id,
                InvoiceId = InvoiceId,
                Description = Description,
                TransactionDateTime = TransactionDateTime,
                Amount = Amount,
                Status = (core.Model.ShopTransactionStatusType)Status
            };
        }
    }
}
